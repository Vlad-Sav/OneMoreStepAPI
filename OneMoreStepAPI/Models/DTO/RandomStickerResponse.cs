using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Models.DTO
{
    public class RandomStickerResponse
    {
        public string Sticker { get; set; }
        public bool AlreadyHave { get; set; }
    }
}
