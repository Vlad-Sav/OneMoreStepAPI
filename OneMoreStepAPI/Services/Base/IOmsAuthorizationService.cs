using OneMoreStepAPI.Models;
using OneMoreStepAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Services.Base
{
    public interface IOmsAuthorizationService
    {
        public Task Register(UserRegisterRequest userRegisterRequest);
        public Task<User> Login(UserLoginRequest userLoginRequest);
        public Task<User> ConfirmLoginData(string email, string password);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
