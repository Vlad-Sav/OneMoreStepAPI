using OneMoreStepAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Models
{
    public class Route: BaseModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string CoordinatesJSON { get; set; }

        [Required]
        public DateTime CreationDateTime { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<RoutesPicture> RoutesPictures { get; set; }
        public virtual ICollection<RoutesLikes> RoutesLikes { get; set; }
    }
}
