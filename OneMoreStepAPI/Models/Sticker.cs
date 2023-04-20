using OneMoreStepAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Models
{
    public class Sticker: BaseModel
    {
        public string Url { get; set; }

        public virtual ICollection<UsersStickers> UsersStickers { get; set; }
    }
}
