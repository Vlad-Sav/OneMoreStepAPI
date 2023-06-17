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
using System.Collections.Generic;
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

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> MyRoutes()
        {
            var res = await _service.MyRoutes(GetUserId());
            return Ok(res);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Routes(int id)
        {
            var res = await _service.MyRoutes(id);
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
            var res = await _service.CreateRoute(routeDTO, GetUserId());
            if (res != null){
                //Adding pictures pathes on Amazon S3 related to route to database
                foreach (var picture in routeDTO.Photos)
                {
                    var pictureName = await SendPictureToAmazonS3(picture);
                    await _service.AddPhoto(res.Id, pictureName);
                }
            }
          
            return Conflict();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Photos(int id)
        {
            var photos = await _service.GetPhotos(id);
            List<string> photosRes = new List<string>();
            foreach(var p in photos)
            {
                byte[] photo = new byte[0];

                try
                {
                    photo = await GetPictureFromAmazonS3(p);
                }
                catch (Exception e)
                {
                
                }

                if (photo != null)
                {
                    var stickerBase64 = Convert.ToBase64String(photo.ToArray());
                    photosRes.Add(stickerBase64);
                }
                
            }
          
            return Ok(new PhotosResponse {Photos = photosRes });
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
        [HttpPost]
        [Route("[action]/{routeId}")]
        public async Task<IActionResult> Like(int routeId)
        {
            var res = await _service.Like(GetUserId(), routeId);
            if (!res) return BadRequest();
            return Ok();
        }

        [HttpPost]
        [Route("[action]/{routeId}")]
        public async Task<IActionResult> Dislike(int routeId)
        {
            var res = await _service.Like(GetUserId(), routeId);
            if (!res) return BadRequest();
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ProfilePhoto([FromBody] StringRequest photo)
        {
            var pictureName = await SendPictureToAmazonS3(photo.Photo);
            await _service.AddPhoto(GetUserId(), pictureName);

            return Ok();
        }
    }
}
