using OneMoreStepAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Services.Base
{
    public interface IStickersService
    {
        public Task<UsersPinnedSticker> GetUserPinnedStickerAsync(int userId);

        public Task<int> GetStickersCount();

        public Task<(int, bool)> RandomSticker(int userId);

        public Task<bool> PinSticker(int userId, int stickerId);

        public Task<List<int>> GetUsersStickers(int userId);

    }
}
