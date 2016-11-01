using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNetHelpers.Helpers.Validators
{
    public static class EmailValidator
    {
        /// <summary>
        /// Validates email address if valid
        /// </summary>
        /// <param name="EmailAddress">Address to validate</param>
        /// <returns><c>true</c> if valid, else <c>false</c></returns>
        public static bool IsEmailValid(string EmailAddress)
        {
            if (string.IsNullOrEmpty(EmailAddress.Trim()))
                return false;

            if (EmailAddress.Contains(" "))
                return false;

            string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                               @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                               @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex re = new Regex(emailRegex);
            if (re.IsMatch(EmailAddress))
                return true;
            else
                return false;
        }
    }
}
