using OneMoreStepAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Models
{
    public class UsersStepsCount : BaseModel
    {
        public int UserId { get; set; }

        public int StepsCount { get; set; }

        public DateTime Date { get; set; }

        public virtual User User { get; set; }
    }
}
