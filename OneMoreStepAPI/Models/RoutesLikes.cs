using OneMoreStepAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Models
{
    public class RoutesLikes: BaseModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RouteId { get; set; }

        [Required]
        public DateTime LikeDateTime { get; set; }

        public virtual Route Route { get; set; }
    }
}
