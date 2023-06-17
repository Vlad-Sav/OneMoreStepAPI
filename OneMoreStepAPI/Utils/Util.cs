using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;

using System.Drawing;
using System.Drawing.Imaging;
using OneMoreStepAPI.Models;

namespace OneMoreStepAPI
{
    public static class Util
    {
        public const int STEPS_FOR_LEVEL = 5000;

        public static MemoryStream Base64StringToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            var resultStream = new MemoryStream();
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                ms.Write(imageBytes, 0, imageBytes.Length);
                resultStream = ms;
            }
            return resultStream;
        }
    }
}
