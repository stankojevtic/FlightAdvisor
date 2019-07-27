using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FlightAdvisor.Core.Helpers.Authorization
{
    public static class HashPasswordHelper
    {
        //public static byte[] Hash(string value, string salt)
        //{
        //    return Hash(Encoding.UTF8.GetBytes(value), Encoding.UTF8.GetBytes(salt));
        //}

        public static string HashPassword(string password, string salt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] src = Encoding.Unicode.GetBytes(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inarray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inarray);
        }

        //public static byte[] Hash(byte[] value, byte[] salt)
        //{
        //    byte[] saltedValue = value.Concat(salt).ToArray();

        //    return new SHA256Managed().ComputeHash(saltedValue);
        //}

        public static string GenerateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[32];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
    }
}
