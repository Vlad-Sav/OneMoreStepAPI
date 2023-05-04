using OneMoreStepAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Services.Base
{
    public interface IRoutesService
    {
        public Task<List<RouteResponse>> GetRoutes();
        public Task<bool> CreateRoute(RouteSaveRequest routeDTO, int userId);
    }
}
