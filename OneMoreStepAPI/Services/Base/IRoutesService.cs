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
        public Task<bool> Like(int userId, int routeId);
        public Task<bool> Unlike(int userId, int routeId);
    }
}
