using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetHelpers.Helpers.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Find and return Index of item in a given list
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="values">Items to look in</param>
        /// <param name="item">Item to look for</param>
        /// <returns>Array of indexes</returns>
        public static int[] FindAllIndexOf<T> (this IEnumerable<T> values, T item)
        {
            return values.Select((b, i) => object.Equals(b, item) ? i : -1).Where(i => i != -1).ToArray();
        }
    }
}
