using System;
using System.Text;

namespace DotNetHelpers.Security
{
    [Obsolete("This class is depreciated, use Security.StringCipher Instead")]
    public class Password
    {
        /// <summary>
        /// Encodes password in Base64 String
        /// </summary>
        /// <param name="UserPassword">Password to encode</param>
        /// <returns>Base64 encoded password</returns>
        /// <exception cref="ArgumentNullException" />
        public static string EncodePassword(string UserPassword)
        {
            if (string.IsNullOrEmpty(UserPassword))
                throw new ArgumentNullException("UserPassword");

            var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(UserPassword), Base64FormattingOptions.None);
            return encoded;
        }

        /// <summary>
        /// Decodes Base64 encoded password
        /// </summary>
        /// <param name="mEncodedPassword">Base64 Encoded password</param>
        /// <exception cref="ArgumentNullException" />
        /// <returns>Decoded password</returns>
        public static string DecodePassword(string mEncodedPassword)
        {
            if (string.IsNullOrEmpty(mEncodedPassword))
                throw new ArgumentNullException("mEncodedPassword");

            return Encoding.UTF8.GetString(Convert.FromBase64String(mEncodedPassword));
        }
    }
}