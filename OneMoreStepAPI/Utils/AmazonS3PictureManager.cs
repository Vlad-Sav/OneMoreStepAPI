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
        public async Task<FileContentResult> DownloadPicture(string filename)
        {
            /*// Введите свои данные для доступа к Amazon S3 в переменных accessKey, secretKey и bucketName
            string accessKey = "AKIAWBN5LTTBJCBFX46S";
            string secretKey = "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";
            string bucketName = "onemorestepbucket";

            // Создаем клиента Amazon S3 с использованием ключа доступа и секретного ключа
            var s3Client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.USEast1);
*/
            // Параметры запроса к Amazon S3
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
                var fileExtension = Path.GetExtension(filename);

                // Возвращаем файл в соответствующем формате
                switch (fileExtension)
                {
                    case ".pdf":
                        return new FileContentResult(fileBytes, "application/pdf");
                    case ".jpg":
                    case ".jpeg":
                        return new FileContentResult(fileBytes, "image/jpeg");
                    case ".png":
                        return new FileContentResult(fileBytes, "image/png");
                    default:
                        return null;
                }
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
