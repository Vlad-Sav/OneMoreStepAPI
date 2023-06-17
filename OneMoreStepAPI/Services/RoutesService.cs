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

        public async Task AddPhoto(int routeId, string photoPath)
        {
            await _dbContext.RoutesPictures.AddAsync(new RoutesPicture { RouteId = routeId, PhotoPath = photoPath});
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }
        }

        public async Task AddProfilePhoto(int userId, string photoPath)
        {
            await _dbContext.ProfilePhotos.AddAsync(new ProfilePhotos() {
                UserId = userId,
                PhotoPath = photoPath
            });

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }
        }


        public async Task<Route> CreateRoute(RouteSaveRequest routeDTO, int userId)
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
                var addedRoute = await _dbContext.Routes.FirstOrDefaultAsync(r => r.Title == routeDTO.RouteTitle &&
                                                                r.Description == routeDTO.RouteDescription &&
                                                                r.UserId == userId);

                return addedRoute;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }

        public async Task<List<string>> GetPhotos(int routeId)
        {
            var photos = await _dbContext.RoutesPictures.Where(r => r.RouteId == routeId).ToListAsync();
            return photos.Select(p => p.PhotoPath).ToList();
        }

        public async Task<List<RouteResponse>> GetRoutes()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<OMSAutoMapper>();
            });
            var mapper = new Mapper(config);
            var res = await _dbContext.Routes.Include("User").Select(r => mapper.Map<RouteResponse>(r)).ToListAsync();
            return res;
        }

        public async Task<bool> Like(int userId, int routeId)
        {
            var isLiked = await _dbContext.RoutesLikes.AnyAsync(l => l.RouteId == routeId && l.UserId == userId);
            if (isLiked) {
                return false;
            }
            try
            {
                await _dbContext.RoutesLikes.AddAsync(new RoutesLikes
                {
                    RouteId = routeId,
                    UserId = userId
                });
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<RouteResponse>> MyRoutes(int userId)
        {

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<OMSAutoMapper>();
            });
            var mapper = new Mapper(config);
            var res = await _dbContext.Routes.Include("User").Where(r => r.UserId == userId).Select(r => mapper.Map<RouteResponse>(r)).ToListAsync();
            return res;
        }

        public async Task<bool> Unlike(int userId, int routeId)
        {
            var isLiked = await _dbContext.RoutesLikes.AnyAsync(l => l.RouteId == routeId && l.UserId == userId);
            if (!isLiked)
            {
                return false;
            }
            try
            {
                var like = await _dbContext.RoutesLikes.FirstOrDefaultAsync(l => l.RouteId == routeId && l.UserId == userId);
                _dbContext.RoutesLikes.Remove(like);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
