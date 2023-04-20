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
using OneMoreStepAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StickersController : AmazonController
    {
        private StickersService _service;
        public StickersController(IConfiguration config, 
            AmazonS3Client amazonClient, 
            BucketName bucketName,
            StickersService service): base(config, amazonClient, bucketName)
        {
            _service = service;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> UsersPinnedSticker()
        {
            var currentUserId = GetUserId();
            var usersPinnedSticker = await _service.GetUserPinnedStickerAsync(currentUserId);

            if (usersPinnedSticker == null)
            {
                return NotFound();
            }

            byte[] sticker;

            try
            {
                sticker  = await GetPictureFromAmazonS3("Stickers/" + usersPinnedSticker.Sticker.Url);
            }
            catch (Exception e)
            {
                return NotFound();
            }

            if(sticker == null)
            {
                return NotFound();
            }
            var stickerBase64 = Convert.ToBase64String(sticker.ToArray());
            return Ok(new UsersPinnedStickerResponse { Sticker = stickerBase64 });
        }
    }
}
