using Microsoft.EntityFrameworkCore;
using OneMoreStepAPI.Data;
using OneMoreStepAPI.Models;
using OneMoreStepAPI.Models.DTO;
using OneMoreStepAPI.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Services
{
    public class StickersService: BaseService, IStickersService
    {
        public StickersService(OneMoreStepAPIDbContext dbContext): base(dbContext)
        {
        }

        public async Task<int> GetStickersCount()
        {
            return await _dbContext.Stickers.CountAsync();
        }

        public async Task<UsersPinnedSticker> GetUserPinnedStickerAsync(int userId)
        {
            var usersPinnedSticker = await _dbContext.UsersPinnedStickers
                .Include(c => c.Sticker)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (usersPinnedSticker == null)
            {
                return null;
            }
            return usersPinnedSticker;
        }

        public async Task<(int, bool)> RandomSticker(int userId)
        {
            var stickersCount = await GetStickersCount();
            var randomStickerId = new Random().Next(1, stickersCount + 1);

            var alreadyHave = await _dbContext
                                        .UsersStickers
                                        .Where(s => s.UserId == userId)
                                        .AnyAsync(s => s.StikerId == randomStickerId);

            if (!alreadyHave) {
                await _dbContext.UsersStickers.AddAsync(
                    new UsersStickers { StikerId = randomStickerId, UserId = userId });
                await _dbContext.SaveChangesAsync();
            }

            return (randomStickerId, alreadyHave);
        }
    }
}
