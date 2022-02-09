using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Infrastructure
{
    public class CustomPasswordHashService : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] arrays = Encoding.UTF8.GetBytes(password);
            arrays = md5.ComputeHash(arrays);
            StringBuilder sb = new StringBuilder();

            foreach (byte ba in arrays)
            {
                // 16lık sayi ssisteminde byte çevir 
                sb.Append(ba.ToString("x2").ToLower());
            }

            return sb.ToString();
        }

    }
}
