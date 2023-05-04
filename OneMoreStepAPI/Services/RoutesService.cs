using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OneMoreStepAPI.Data;
using OneMoreStepAPI.Models;
using OneMoreStepAPI.Models.DTO;
using OneMoreStepAPI.Services.Base;
using OneMoreStepAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Services
{
    public class RoutesService : BaseService, IRoutesService
    {
        public RoutesService(OneMoreStepAPIDbContext dbContext): base(dbContext)
        {
        }

        public async Task<bool> CreateRoute(RouteSaveRequest routeDTO, int userId)
        {
            var route = new Route
            {
                Title = routeDTO.RouteTitle,
                Description = routeDTO.RouteDescription,
                UserId = userId,
                CoordinatesJSON = JsonSerializer.Serialize(routeDTO.Coordinates)
            };

            await _dbContext.AddAsync(route);

            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<List<RouteResponse>> GetRoutes()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<OMSAutoMapper>();
            });
            var mapper = new Mapper(config);
            var res = await _dbContext.Routes.Select(r => mapper.Map<RouteResponse>(r)).ToListAsync();
            return res;
        }
    }
}
