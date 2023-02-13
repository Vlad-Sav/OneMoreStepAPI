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

namespace OneMoreStepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private IConfiguration _config;

        private OneMoreStepAPIDbContext _dbContext;

        public AuthorizationController(IConfiguration config, OneMoreStepAPIDbContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] UserDTO userLogin)
        {
            var hash = CreatePasswordHash(userLogin.Password);
            userLogin.Password = hash;
            // to do: add to database
            await _dbContext.AddAsync(new User { Username = userLogin.Username, PasswordHash = userLogin.Password });
            await _dbContext.SaveChangesAsync();

            return Ok(userLogin);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTO userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = GenerateJwtToken(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
               // new Claim(ClaimTypes.Email, user.EmailAddress),
               // new Claim(ClaimTypes.GivenName, user.GivenName),
               // new Claim(ClaimTypes.Surname, user.Surname),
               // new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(UserDTO userLogin)
        {
            //  var currentUser = UserConstants.Users.FirstOrDefault(o => o.Username.ToLower() == userLogin.Username.ToLower() && o.Password == userLogin.Password);
            /*
            if (currentUser != null)
            {
                return currentUser;
            }
            */
            return null;
        }
    }
}
