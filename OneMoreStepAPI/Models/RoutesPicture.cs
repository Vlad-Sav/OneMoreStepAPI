using OneMoreStepAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Models
{
    public class RoutesPicture: BaseModel
    {
        public int RouteId { get; set; }

        public string PhotoPath { get; set; }

        public virtual Route Route { get; set; }
    }
}
