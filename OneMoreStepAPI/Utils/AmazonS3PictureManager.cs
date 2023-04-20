using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Utils
{
    public class AmazonS3PictureManager : BasePictureManager
    {
        private AmazonS3Client _client;
        private string _bucketName;
        public AmazonS3PictureManager(AmazonS3Client client, string bucketName)
        {
            _client = client;
            _bucketName = bucketName;
        }

        public async Task<byte[]> DownloadPicture(string filename)
        {
            var request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = filename
            };

            // Получаем файл с Amazon S3
            using (var response = await _client.GetObjectAsync(request))
            {
                var fileStream = new MemoryStream();

                response.ResponseStream.CopyTo(fileStream);
                var fileBytes = fileStream.ToArray();
                return fileBytes;
            }
        }

        public async Task<string> UploadPicture(string base64Image)
        {
            var imageStream = Util.Base64StringToImage(base64Image);

            // Генерируем уникальное имя файла для изображения
            string fileName = Guid.NewGuid().ToString() + ".jpeg";

            // Загружаем изображение на Amazon S3
            using (Stream stream = new MemoryStream(imageStream.ToArray()))
            {
                PutObjectRequest request = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = fileName,
                    InputStream = stream
                };

                await _client.PutObjectAsync(request);
            }

            // Сохраняем имя файла в объекте MyObject
            return fileName;
        }
    }
}
