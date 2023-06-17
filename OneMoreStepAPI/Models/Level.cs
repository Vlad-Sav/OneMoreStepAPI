using OneMoreStepAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Models
{
    public class Level: BaseModel
    {
        public int UserId { get; set; }
        public int Lvl { get; set; }
        public virtual User User { get; set; }
    }
}
