using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class RouteController : BaseController
    {
        private IConfiguration _config;

        private OneMoreStepAPIDbContext _dbContext;

        public RouteController(IConfiguration config, OneMoreStepAPIDbContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoute([FromBody] RouteDTO routeDTO)
        {
            var route = new Route
            {
                Title = routeDTO.Title,
                Description = routeDTO.Description,
                UserId = GetUserId(),
                CoordinatesJSON = JsonSerializer.Serialize(routeDTO.Coordinates)
            };

            await _dbContext.AddAsync(route);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

    }
}
