using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OneMoreStepAPI
{
    public class Util
    {
        public static string CreatePasswordHash(string password)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(password));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}
