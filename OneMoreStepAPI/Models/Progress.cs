using OneMoreStepAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Models
{
    public class Progress: BaseModel
    {
        public int UserId { get; set; }
        public int UsersProgress { get; set; }
        public virtual User User { get; set; }
    }
}
