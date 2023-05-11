using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OneMoreStepAPI.Controllers.Base;
using OneMoreStepAPI.Data;
using OneMoreStepAPI.Models;
using OneMoreStepAPI.Models.DTO;
using OneMoreStepAPI.Services;
using OneMoreStepAPI.Services.Base;
using OneMoreStepAPI.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StepsController : BaseController
    { 
        private IStepsService _service;

        public StepsController(IConfiguration config, IStepsService service): base(config)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepsCount"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateStepsCount([FromBody] UpdateStepsCountRequest stepsCount)
        {
            var currentUserId = GetUserId();
            var result = await _service.UpdateStepsCountAsync(currentUserId, stepsCount.StepsCount);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="daysNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> UsersStepsCount([FromQuery] int userId, [FromQuery] int daysNumber)
        {
            var usersStepsCount = await _service.GetUsersStepsCount(userId, daysNumber);
            
            if (usersStepsCount < 0) return BadRequest();

            return Ok(usersStepsCount);        
        }
    }
}
