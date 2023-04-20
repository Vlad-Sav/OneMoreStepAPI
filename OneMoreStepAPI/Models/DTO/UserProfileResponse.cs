using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Models.DTO
{
    public class UserProfileResponse : ControllerBase
    {
        public string Username { get; set; }

        //public List<RouteDTO> Routes { get; set; }
    }
}
