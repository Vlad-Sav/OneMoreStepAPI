using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OneMoreStepAPI.Controllers.Base;
using OneMoreStepAPI.Data;
using OneMoreStepAPI.Models;
using OneMoreStepAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private IConfiguration _config;

        private OneMoreStepAPIDbContext _dbContext;

        public UserController(IConfiguration config, OneMoreStepAPIDbContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] int id)
        {


            return Ok("hello");
        }

        /// <summary>
        /// Returns Information about user including username and routes
        /// </summary>
        /// <param name="id">Id of Required User</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetUser([FromQuery] int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            
            if (user == null) return NotFound();
            
            List<RouteDTO> routes = await _dbContext.Routes.Where(r => r.UserId == user.Id).Select(r => new RouteDTO
            {
                Title = r.Title,
                Description = r.Description,
                Coordinates = JsonSerializer.Deserialize<List<LatLong>>(r.CoordinatesJSON, null)
            }).ToListAsync();

            var userProfile = new UserProfileDTO
            {
                Username = user.Username,
                Routes = routes
            };

            return Ok(userProfile);
        }
    }
}
