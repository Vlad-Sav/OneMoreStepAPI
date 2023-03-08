using Amazon.S3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OneMoreStepAPI.Controllers.Base;
using OneMoreStepAPI.Data;
using OneMoreStepAPI.Models;
using OneMoreStepAPI.Models.DTO;
using OneMoreStepAPI.Utils;
using System;
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

        private AmazonS3Client _amazonClient;

        private string _bucketName;

        public RouteController(IConfiguration config, OneMoreStepAPIDbContext dbContext, AmazonS3Client amazonClient)
        {
            _config = config;
            _dbContext = dbContext;
            _amazonClient = amazonClient;
            _bucketName = config["AmazonS3:BucketName"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateRoute([FromBody] RouteDTO routeDTO)
        {
            var route = new Route
            {
                Title = routeDTO.Title,
                Description = routeDTO.Description,
                UserId = GetUserId(),
                CoordinatesJSON = JsonSerializer.Serialize(routeDTO.Coordinates)
            };

            var addedRoute = await _dbContext.AddAsync(route);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            
            //Adding pictures pathes on Amazon S3 related to route to database
            foreach (var picture in routeDTO.PicturesBase64)
            {
                var pictureName = await SendPictureToAmazonS3(picture);
                var routePicture = new RoutesPicture
                {
                    RouteId = addedRoute.Entity.Id,
                    PhotoPath = pictureName
                };
                await _dbContext.AddAsync(route);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateRoute(int id, [FromBody] RouteDTO route)
        {
            var existingRoute = await _dbContext.Routes.FindAsync(id);

            if (existingRoute == null)
            {
                return NotFound();
            }

            existingRoute.Title = route.Title;
            existingRoute.Description = route.Description;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok(existingRoute);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            var route = await _dbContext.Routes.FindAsync(id);

            if (route == null)
            {
                return NotFound();
            }

            using var transaction = _dbContext.Database.BeginTransaction();

            try
            { 
                //Deleting photos from database
                var routesPicturesToDelete = await _dbContext.RoutesPictures
                    .Where(rp => rp.RouteId == id)
                    .ToListAsync();
                _dbContext.RoutesPictures.RemoveRange(routesPicturesToDelete);
                await _dbContext.SaveChangesAsync();

                _dbContext.Routes.Remove(route);
                await _dbContext.SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception)
            {
                return NotFound();
            }
           
            return Ok(route);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<IActionResult> GetPictureFromAmazonS3(string filename)
        {
            AmazonS3PictureManager manager = new AmazonS3PictureManager(_amazonClient, _bucketName);

            var res = await manager.DownloadPicture(filename);

            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64File"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<string> SendPictureToAmazonS3(string base64File)
        {
            AmazonS3PictureManager manager = new AmazonS3PictureManager(_amazonClient, _bucketName);

            var res = await manager.UploadPicture(base64File);

            return res;
        }
    }
}
