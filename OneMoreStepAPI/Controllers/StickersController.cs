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
using System.Threading.Tasks;

namespace OneMoreStepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StickersController : AmazonController
    {
        private IStickersService _service;
        public StickersController(IConfiguration config, 
            AmazonS3Client amazonClient, 
            BucketName bucketName,
            IStickersService service): base(config, amazonClient, bucketName)
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

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> StickersNumber()
        {
            try
            {
                var res = await _service.GetStickersCount();
                if (res == null) return BadRequest();
                return Ok(new StickersNumberResponse() { StickersNumber = res });
            }
            catch
            { 
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> UsersStickers()
        {
            try
            {
                var res = await _service.GetUsersStickers(GetUserId());
                if (res == null) return BadRequest();
                return Ok(new IntListResponse() { StickerIds = res });
            }
            catch
            {
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Sticker(int id)
        {
            byte[] sticker;

            try
            {
                sticker = await GetPictureFromAmazonS3($"Stickers/{id}.png");
            }
            catch (Exception e)
            {
                return NotFound();
            }

            if (sticker == null)
            {
                return NotFound();
            }
            var stickerBase64 = Convert.ToBase64String(sticker.ToArray());
            return Ok(new UsersPinnedStickerResponse { Sticker = stickerBase64 });
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> RandomSticker()
        {
            var userId = GetUserId();
            var res = await _service.RandomSticker(userId);

            byte[] sticker;

            try
            {
                sticker = await GetPictureFromAmazonS3($"Stickers/{res.Item1}.png");
            }
            catch (Exception e)
            {
                return NotFound();
            }

            if (sticker == null)
            {
                return NotFound();
            }
            var stickerBase64 = Convert.ToBase64String(sticker.ToArray());
            return Ok(new RandomStickerResponse { Sticker = stickerBase64, AlreadyHave = res.Item2 });
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> PinSticker([FromBody] StickerRequest stickerRequest)
        {
            var userId = GetUserId();
            await _service.PinSticker(userId, stickerRequest.Id);
            return Ok();
        }
        /* [HttpGet]
         [Route("[action]")]
         public async Task<IActionResult> RandomSticker()
         {
             var userId = GetUserId();
             var res = await _service.RandomSticker(userId);

             byte[] sticker;


             var stickerBase64 = Convert.ToBase64String(sticker.ToArray());
             return Ok(new RandomStickerResponse { Sticker = stickerBase64, AlreadyHave = res.Item2 });
         }*/

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UsersPinnedSticker(int id)
        {
            var usersPinnedSticker = await _service.GetUserPinnedStickerAsync(id);

            if (usersPinnedSticker == null)
            {
                return NotFound();
            }

            byte[] sticker;

            try
            {
                sticker = await GetPictureFromAmazonS3("Stickers/" + usersPinnedSticker.Sticker.Url);
            }
            catch (Exception e)
            {
                return NotFound();
            }

            if (sticker == null)
            {
                return NotFound();
            }
            var stickerBase64 = Convert.ToBase64String(sticker.ToArray());
            return Ok(new UsersPinnedStickerResponse { Sticker = stickerBase64 });
        }

    }
}
