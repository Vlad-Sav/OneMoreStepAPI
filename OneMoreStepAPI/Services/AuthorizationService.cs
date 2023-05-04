using Microsoft.EntityFrameworkCore;
using OneMoreStepAPI.Data;
using OneMoreStepAPI.Models;
using OneMoreStepAPI.Models.DTO;
using OneMoreStepAPI.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Services
{
    public class AuthorizationService : BaseService, IOmsAuthorizationService
    {
        public AuthorizationService(OneMoreStepAPIDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Register(UserRegisterRequest userRegisterRequest)
        {

            if (await _dbContext.Users.AnyAsync(u => u.Email == userRegisterRequest.Email))
            {
                throw new InvalidOperationException("User with this Email already exists.");
            }

            if (await _dbContext.Users.AnyAsync(u => u.Username == userRegisterRequest.Username))
            {
                throw new InvalidOperationException("User with this Username already exists.");
            }

            CreatePasswordHash(userRegisterRequest.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt);

            var user = new User
            {
                Email = userRegisterRequest.Email,
                Username = userRegisterRequest.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> Login(UserLoginRequest userLoginRequest)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userLoginRequest.Email);
            return user;
        }

        public async Task<User> ConfirmLoginData(string email, string password)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(o =>
                o.Email.ToLower() == email.ToLower()
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
