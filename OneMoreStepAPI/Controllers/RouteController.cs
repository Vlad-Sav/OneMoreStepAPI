using Amazon.S3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OneMoreStepAPI.Controllers.Base;
using OneMoreStepAPI.Data;
using OneMoreStepAPI.Models;
using OneMoreStepAPI.Models.DTO;
using OneMoreStepAPI.Models.Settings;
using OneMoreStepAPI.Services;
using OneMoreStepAPI.Services.Base;
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
    public class RouteController : AmazonController
    {
        private IRoutesService _service;

        public RouteController(IConfiguration config, 
            IRoutesService service,
            AmazonS3Client amazonClient, 
            BucketName bucketName): base(config, amazonClient, bucketName)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoutes()
        {
            var res = await _service.GetRoutes();
            return Ok(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRoute([FromBody] RouteSaveRequest routeDTO)
        {
            if (await _service.CreateRoute(routeDTO, GetUserId())){
                return Ok();
            }
            //Adding pictures pathes on Amazon S3 related to route to database
            /* foreach (var picture in routeDTO.PicturesBase64)
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
             }*/
            return Conflict();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="route"></param>
        /// <returns></returns>
       /* [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateRoute(int id, [FromBody] RouteSaveRequest route)
        {
            *//*var existingRoute = await _dbContext.Routes.FindAsync(id);

            if (existingRoute == null)
            {
                return NotFound();
            }

            existingRoute.Title = route.RouteTitle;
            existingRoute.Description = route.RouteDescription;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }existingRoute*//*

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteRoute([FromQuery] int id)
        {
          *//*  var route = await _dbContext.Routes.FindAsync(id);

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
           *//*
            return Ok();
        }*/
    }
}
