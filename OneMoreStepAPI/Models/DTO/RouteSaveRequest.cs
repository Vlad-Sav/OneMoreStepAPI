using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Models.DTO
{

    public class RouteSaveRequest
    {
        public string RouteTitle { get; set; }

        public string RouteDescription { get; set; }

        public List<LatLng> Coordinates { get; set; }

        public List<string> Photos { get; set; }
    }
}
