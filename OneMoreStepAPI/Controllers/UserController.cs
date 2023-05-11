using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OneMoreStepAPI.Controllers.Base;
using OneMoreStepAPI.Data;
using OneMoreStepAPI.Models;
using OneMoreStepAPI.Models.DTO;
using OneMoreStepAPI.Services.Base;
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
        private IUserService _service;

        public UserController(IConfiguration config, IUserService service): base(config)
        {
            _config = config;
            _service = service;
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
            var userId = GetUserId();
            var user = await _service.GetUser(userId);
            
            if (user == null) return NotFound();

            var userProfile = await _service.UserProfile(user);
            
            return Ok(userProfile);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> TopUsers()//[FromQuery] string period)
        {
            var res = await _service.GetTopUser();
            return Ok(res);
          /*  DateTime fromDate;
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

            return Ok(users);*/
        }
    }
}
