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
    public class StepsController : BaseController
    {
        private IConfiguration _config;

        private OneMoreStepAPIDbContext _dbContext;

        public StepsController(IConfiguration config, OneMoreStepAPIDbContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStepsCount([FromBody] int stepsCount)
        {
            var currentUserId = GetUserId();
            UsersStepsCount userStepsCount = await _dbContext.UsersStepsCount.FirstOrDefaultAsync(c => (c.UserId == currentUserId)
                                                                                && (c.Date.Date == DateTime.Today));

            if (userStepsCount == null)
            {
                userStepsCount = new UsersStepsCount
                {
                    UserId = GetUserId(),
                    StepsCount = stepsCount,
                    Date = DateTime.Today
                };
                _dbContext.UsersStepsCount.Add(userStepsCount);
                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

            }
            else
            {
                _dbContext.UsersStepsCount.Update(userStepsCount);
                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            
            return Ok();
        }
    }
}
