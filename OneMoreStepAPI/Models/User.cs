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
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; } = new byte[32];

        [Required]
        public byte[] PasswordSalt { get; set; } = new byte[32];

        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<UsersStepsCount> UsersStepsCounts { get; set; }
        public virtual ICollection<UsersStickers> UsersStickers { get; set; }
        public virtual UsersPinnedSticker UserPinnedSticker { get; set; }
    }
}
