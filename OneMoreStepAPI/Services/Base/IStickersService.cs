using OneMoreStepAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Services.Base
{
    interface IStickersService
    {
        public Task<UsersPinnedSticker> GetUserPinnedStickerAsync(int userId);
    }
}
