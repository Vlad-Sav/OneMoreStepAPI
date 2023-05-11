using OneMoreStepAPI.Models;
using OneMoreStepAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Services.Base
{
    public interface IUserService
    {
        public Task<UserProfileResponse> UserProfile(User user);
        public Task<User> GetUser(int userId);
        public Task<List<Top3UserResponse>> GetTopUser();
    }
}
