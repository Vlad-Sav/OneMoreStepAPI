using Microsoft.EntityFrameworkCore;
using OneMoreStepAPI.Data;
using OneMoreStepAPI.Models;
using OneMoreStepAPI.Models.DTO;
using OneMoreStepAPI.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Services
{
    public class UserService: BaseService, IUserService
    {
        public UserService(OneMoreStepAPIDbContext context): base(context)
        {

        }

        public async Task<List<Top3UserResponse>> GetTopUser()
        {
            var res = await _dbContext
                                .UsersStepsCount
                                .Include("User")
                                .Where(u => u.Date == DateTime.Today)
                                .OrderByDescending(u => u.StepsCount)
                                .Take(3)
                                .Select(u => new Top3UserResponse
                                {
                                    UserId = u.UserId,
                                    StepsCount = u.StepsCount,
                                    UserName = u.User.Username
                                })
                                .ToListAsync();
            return res;
        }

        public async Task<User> GetUser(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

     /*   public async Task<UserProfileResponse> UserProfile(int userId)
        {
            List<RouteSaveRequest> routes = _dbContext.Routes.Where(r => r.UserId == userId).Select(r => new RouteSaveRequest
            {
                RouteTitle = r.Title,
                RouteDescription = r.Description,
                Coordinates = JsonSerializer.Deserialize<List<LatLng>>(r.CoordinatesJSON, null)
            }).ToList();
            var user = GetUser(userId).Result;
            var userProfile = new UserProfileResponse
            {
                Username = user.Username,
                //Routes = routes
            };
            return userProfile;
        }*/
        public async Task<UserProfileResponse> UserProfile(User user)
        {
            var routes = await _dbContext.Routes.Where(r => r.UserId == user.Id).Select(r => new RouteSaveRequest
            {
                RouteTitle = r.Title,
                RouteDescription = r.Description,
                Coordinates = JsonSerializer.Deserialize<List<LatLng>>(r.CoordinatesJSON, null)
            }).ToListAsync();
            var userProfile = new UserProfileResponse
            {
                Username = user.Username
            };
            return userProfile;
        }
    }
}
