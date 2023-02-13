﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Models
{
    public class User
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        //public string PasswordSalt { get; set; }
        //public string EmailAddress { get; set; }
        //public string Role { get; set; }
        //public string Surname { get; set; }
        //public string GivenName { get; set; }
    }
}