using OneMoreStepAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Models
{
    public class UsersPinnedSticker : BaseModel
    {
        public int UserId { get; set; }

        public int StickerId { get; set; }

        public virtual User User { get; set; }

        public virtual Sticker Sticker { get; set; }
    }
}
