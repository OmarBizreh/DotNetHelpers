using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetHelpers.Exceptions
{
    public class InvalidInputException : Exception
    {
        /// <summary>
        /// Input that caused this exception
        /// </summary>
        public string InvalidInput { get; set; }
        /// <summary>
        /// Create a new instance of <see cref="InvalidInputException"/> class
        /// </summary>
        /// <param name="InvalidInput">Input that caused this exception</param>
        public InvalidInputException(string InvalidInput) : base()
        {
            this.InvalidInput = InvalidInput;
        }
    }
}
