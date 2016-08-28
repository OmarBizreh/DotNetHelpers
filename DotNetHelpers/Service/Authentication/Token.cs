﻿using System;
using System.Collections.Generic;
using DotNetHelpers.Helpers.Extensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetHelpers.Security;

namespace DotNetHelpers.Service.Authentication
{
    public class Token
    {
        /// <summary>
        /// Seperator <see cref="byte"/> used to seperate values in Token
        /// </summary>
        private static byte TokenSeperator
        {
            get
            {
                return byte.Parse("00");
            }
        }

        /// <summary>
        /// Token generated
        /// </summary>
        public string AuthenticationToken { get; }
        /// <summary>
        /// Args used to create the token
        /// </summary>
        public Dictionary<string, string> Arguments { get; set; }

        /// <summary>
        /// Return Encoding type used to generate Authentication Token
        /// </summary>
        public AuthenticationEnums.EncodingType Encoding
        {
            get
            {
                return AuthenticationEnums.EncodingType.UTF8;
            }
        }
        /// <summary>
        /// Create new instance of <see cref="Token"/> class and generates a new <see cref="AuthenticationToken"/>
        /// </summary>
        /// <param name="_Args">Combination of parameters to generate token from</param>
        public Token(Dictionary<string, string> _Args, string EncryptionPass)
        {
            // Can't generate a token without params
            if (_Args.Count < 2)
                throw new ArgumentException("At least two parameters must be in the dictionary", "_Params");

            this.Arguments = _Args;
            this.AuthenticationToken = StringCipher.Encrypt(this.GenerateToken(_Args), EncryptionPass);
        }

        /// <summary>
        /// Generate AuthenticationToken from given parameters
        /// </summary>
        /// <param name="_Args">Combination of paramters to generate token from</param>
        private string GenerateToken(Dictionary<string, string> _Args)
        {
            List<byte> TokenBytes = new List<byte>();
            foreach (var item in _Args)
            {
                TokenBytes.AddRange(System.Text.Encoding.UTF8.GetBytes(string.Join(":", item.Key, item.Value)));
                TokenBytes.Add(Token.TokenSeperator);
            }

            // No need to keep the last seperator value
            TokenBytes.RemoveAt(TokenBytes.Count - 1);

            return Convert.ToBase64String(TokenBytes.ToArray());
        }

        /// <summary>
        /// Decodes token generated by <see cref="Token"/> Class
        /// </summary>
        /// <param name="UserToken"><see cref="Token"/> to decode</param>
        /// <returns>Params used in token</returns>
        public static Token DecodeToken(string UserToken, string EncryptionPass)
        {
            if (string.IsNullOrEmpty(EncryptionPass))
                throw new ArgumentNullException("EncryptionPass");

            UserToken = StringCipher.Decrypt(UserToken, EncryptionPass);

            Dictionary<string, string> tokenParams = new Dictionary<string, string>();

            byte[] TokenBytes = Convert.FromBase64String(UserToken);

            int[] indexes = TokenBytes.FindAllIndexOf(Token.TokenSeperator);

            // If there is no index of seperator, token is invalid
            if (indexes.Length == 0)
                throw new System.ArgumentException("Not a valid Token", "UserToken");

            for (int i = 0; i < indexes.Length; i++)
            {
                string key, value, decodedString;
                byte[] itemBytes;
                if (i != 0)
                    itemBytes = TokenBytes.Where((b, j) => j > indexes[i - 1] && j < indexes[i]).ToArray();
                else
                    itemBytes = TokenBytes.Where((b, j) => j < indexes[i]).ToArray();

                decodedString = System.Text.Encoding.UTF8.GetString(itemBytes);

                // if there is no key, value seperator token is also invalid
                if (!decodedString.Contains(':'))
                    throw new System.ArgumentException("Not a valid Token", "UserToken");

                key = decodedString.Split(':')[0];
                value = decodedString.Split(':')[1];
                tokenParams.Add(key, value);

                if (i == indexes.Length - 1)
                {
                    itemBytes = TokenBytes.Where((b, j) => j > indexes[i]).ToArray();

                    decodedString = System.Text.Encoding.UTF8.GetString(itemBytes);

                    // if there is no key, value seperator token is also invalid
                    if (!decodedString.Contains(':'))
                        throw new System.ArgumentException("Not a valid Token", "UserToken");

                    key = decodedString.Split(':')[0];
                    value = decodedString.Split(':')[1];
                    tokenParams.Add(key, value);
                }
            }
            return new Token(tokenParams, EncryptionPass);
        }
    }
}
