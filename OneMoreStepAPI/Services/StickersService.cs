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
    }
}
