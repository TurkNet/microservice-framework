using System;
using System.Security.Cryptography;
using Noctools.Utils.Extensions;

namespace Noctools.Utils.Helpers
{
    public class CryptoRandomHelper
    {
        private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

        public static byte[] CreateRandomBytes(int length)
        {
            var bytes = new byte[length];
            _rng.GetBytes(bytes);

            return bytes;
        }

        public static string CreateRandomKey(int length)
        {
            var bytes = new byte[length];
            _rng.GetBytes(bytes);

            return Convert.ToBase64String(CreateRandomBytes(length));
        }

        public static string CreateUniqueKey(int length = 8)
        {
            return CreateRandomBytes(length).ToHexString();
        }

        public static string CreateSeriesNumber(string prefix = "MSK")
        {
            return $"{prefix}{DateTime.Now.ToString("yyyyMMddHHmmss")}{CreateUniqueKey()}";
        }
    }
}
