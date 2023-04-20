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
    [Authorize]
    public class UserController : BaseController
    {
        private OneMoreStepAPIDbContext _dbContext;

        public UserController(IConfiguration config, OneMoreStepAPIDbContext dbContext): base(config)
        {
            _config = config;
            _dbContext = dbContext;
        }

        [HttpGet]
        [AllowAnonymous]
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
        public async Task<ActionResult> UserProfile()
        {
            var user = await _dbContext.Users.FindAsync(GetUserId());
            
            if (user == null) return NotFound();
            
            List<RouteSaveRequest> routes = await _dbContext.Routes.Where(r => r.UserId == user.Id).Select(r => new RouteSaveRequest
            {
                RouteTitle = r.Title,
                RouteDescription = r.Description,
                Coordinates = JsonSerializer.Deserialize<List<LatLng>>(r.CoordinatesJSON, null)
            }).ToListAsync();

            var userProfile = new UserProfileResponse
            {
                Username = user.Username,
                //Routes = routes
            };

            return Ok(userProfile);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<User>>> TopByStepsCount([FromQuery] string period)
        {
            DateTime fromDate;
            switch (period.ToLower())
            {
                case "today":
                    fromDate = DateTime.Today;
                    break;
                case "week":
                    fromDate = DateTime.Today.AddDays(-7);
                    break;
                case "month":
                    fromDate = DateTime.Today.AddMonths(-1);
                    break;
                default:
                    return BadRequest("Invalid period specified.");
            }

            var users = await _dbContext.Users
                .Include(u => u.UsersStepsCounts)
                .Where(u => u.UsersStepsCounts.Any(usc => usc.Date >= fromDate))
                .OrderByDescending(u => u.UsersStepsCounts.Where(usc => usc.Date >= fromDate).Sum(usc => usc.StepsCount))
                .ToListAsync();

            return Ok(users);
        }
    }
}
