using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DotNetHelpers.Helpers.Utils
{
    public class Randomizer
    {
        /// <summary>
        /// Generate a random string with the specified MaxSize
        /// </summary>
        /// <param name="maxSize">Max size of the string to generate</param>
        /// <returns>Random string of 'maxSize' length</returns>
        public static string RandomString(int maxSize)
        {
            char[] chars = new char[10];
            chars =
            "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            byte[] data = new byte[1];
            RandomNumberGenerator crypto = RandomNumberGenerator.Create();
            crypto.GetBytes(data);
            data = new byte[maxSize];
            crypto.GetBytes(data);

            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
