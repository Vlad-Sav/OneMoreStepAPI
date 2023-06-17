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
        public async Task<IActionResult> TopUsers()
        {
            var res = await _service.GetTopUser();
            return Ok(res);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> TopUsersForWeek()
        {
            var res = await _service.GetTopUserForWeek();
            return Ok(res);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> TopUsersForMonth()
        {
            var res = await _service.GetTopUserForMonth();
            return Ok(res);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult> UserProfile(int id)
        {
            var user = await _service.GetUser(id);

            if (user == null) return NotFound();

            var userProfile = await _service.UserProfile(user);

            return Ok(userProfile);
        }

       
    }
}
