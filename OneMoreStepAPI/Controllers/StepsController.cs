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
            var delta = await _service.UpdateStepsCountAsync(currentUserId, stepsCount.StepsCount);
            var result = await _service.UpdateProgressAndLevel(currentUserId, delta);
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
        public async Task<IActionResult> UsersStepsCount()
        {
            var usersStepsCount = await _service.GetUsersStepsCount(GetUserId());

            return Ok(usersStepsCount);        
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Progress()
        {
            var progress = await _service.GetUsersProgress(GetUserId());

            if (progress < 0) return BadRequest();

            return Ok(new IntResponse() { Value = progress } );
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Level()
        {
            var level = await _service.GetUsersLevel(GetUserId());

            if (level < 0) return BadRequest();

            return Ok(new IntResponse() { Value = level });
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Chests()
        {
            var chests = await _service.GetUsersChestsCount(GetUserId());

            if (chests < 0) return BadRequest();

            return Ok(new IntResponse() { Value = chests });
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Level(int id)
        {
            var level = await _service.GetUsersLevel(id);

            if (level < 0) return BadRequest();

            return Ok(new IntResponse() { Value = level });
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UsersStepsCount(int id)
        {
            var usersStepsCount = await _service.GetUsersStepsCount(id);

            return Ok(usersStepsCount);
        }

    }
}
