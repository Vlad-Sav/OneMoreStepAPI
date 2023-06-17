using OneMoreStepAPI.Models;
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
        public Task<List<RouteResponse>> MyRoutes(int userId);
        public Task<Route> CreateRoute(RouteSaveRequest routeDTO, int userId);
        public Task AddPhoto(int routeId, string photoPath);
        public Task<bool> Like(int userId, int routeId);
        public Task<bool> Unlike(int userId, int routeId);
        public Task<List<string>> GetPhotos(int userId);
        public Task AddProfilePhoto(int userId, string photoPath);
    }
}
