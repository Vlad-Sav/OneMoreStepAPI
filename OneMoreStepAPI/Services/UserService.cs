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

        public async Task<User> GetUser(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<UserProfileResponse> UserProfile(int userId)
        {
            List<RouteSaveRequest> routes = await _dbContext.Routes.Where(r => r.UserId == userId).Select(r => new RouteSaveRequest
            {
                RouteTitle = r.Title,
                RouteDescription = r.Description,
                Coordinates = JsonSerializer.Deserialize<List<LatLng>>(r.CoordinatesJSON, null)
            }).ToListAsync();
            var user = await GetUser(userId);
            var userProfile = new UserProfileResponse
            {
                Username = user.Username,
                //Routes = routes
            };
            return userProfile;
        }
    }
}
