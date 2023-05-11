using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Models.DTO
{
    public class Top3UserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int StepsCount { get; set; }
    }
}
