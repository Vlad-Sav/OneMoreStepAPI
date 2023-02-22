using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Models.DTO
{

    public class RouteDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<LatLong> Coordinates { get; set; }
    }
}
