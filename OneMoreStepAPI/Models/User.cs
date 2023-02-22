using OneMoreStepAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Models
{
    public class User: BaseModel
    {
        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public virtual ICollection<Route> Books { get; set; }

        //public string PasswordSalt { get; set; }
        //public string EmailAddress { get; set; }
        //public string Role { get; set; }
        //public string Surname { get; set; }
        //public string GivenName { get; set; }
    }
}
