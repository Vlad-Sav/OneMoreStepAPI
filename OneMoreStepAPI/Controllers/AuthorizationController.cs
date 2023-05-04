using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OneMoreStepAPI.Models;
using OneMoreStepAPI.Models.DTO;
using static OneMoreStepAPI.Util;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using OneMoreStepAPI.Data;
using Microsoft.EntityFrameworkCore;
using OneMoreStepAPI.Controllers.Base;
using System.Security.Cryptography;
using OneMoreStepAPI.Services;
using OneMoreStepAPI.Services.Base;

namespace OneMoreStepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : BaseController
    {
        private IOmsAuthorizationService _service;

        public AuthorizationController(IConfiguration config, IOmsAuthorizationService service): base(config)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<User>> Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            try
            {
                await _service.Register(userRegisterRequest);

                var user = await ConfirmLoginData(userRegisterRequest.Email, userRegisterRequest.Password);

                if (user != null)
                {
                    return Ok();
                }
            }
            catch (Exception){}

            return Conflict("User is Not Registered");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLoginRequest)
        {
            User user = null;
            try
            {
                user = await _service.Login(userLoginRequest);
                if (user == null)
                {
                    return BadRequest("User is not found.");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
          
            user = await ConfirmLoginData(userLoginRequest.Email, userLoginRequest.Password);

            if (user != null)
            {
                var token = GenerateJwtToken(user);
                return Ok(new JwtTokenResponse() { Token = token });
            }

            return BadRequest("Wrong Email Or Password");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim("UserId", user.Id + ""),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddDays(7),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        private async Task<User> ConfirmLoginData(string email, string password)
        {
            var currentUser = await _service.ConfirmLoginData(email, password);
            
            if (currentUser == null)
            {
                return null;
            }

            if(!VerifyPasswordHash(password, currentUser.PasswordHash, currentUser.PasswordSalt))
            {
                return null;
            }

            return currentUser;    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
