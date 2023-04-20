using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OneMoreStepAPI.Models.Settings;
using OneMoreStepAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Controllers.Base
{
    public class AmazonController: BaseController
    {
        protected AmazonS3Client _amazonClient;

        protected string _bucketName;

        public AmazonController(IConfiguration config, AmazonS3Client amazonClient, BucketName bucketName): base(config)
        {
            _amazonClient = amazonClient;
            _bucketName = bucketName.BuckName;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [NonAction]
        protected async Task<byte[]> GetPictureFromAmazonS3(string filename)
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
        protected async Task<string> SendPictureToAmazonS3(string base64File)
        {
            AmazonS3PictureManager manager = new AmazonS3PictureManager(_amazonClient, _bucketName);

            var res = await manager.UploadPicture(base64File);

            return res;
        }
    }
}
