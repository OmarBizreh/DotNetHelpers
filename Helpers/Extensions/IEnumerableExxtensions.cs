using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetHelpers.Helpers.Extensions
{
    public static class IEnumerableExxtensions
    {
        /// <summary>
        /// Find and return Index of item in a given list
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="values">Items to look in</param>
        /// <param name="item">Item to look for</param>
        /// <returns>Array of indexes</returns>
        public static int[] FindAllIndexOf<T>(this IEnumerable<T> values, T item)
        {
            return values.Select((b, i) => object.Equals(b, item) ? i : -1).Where(i => i != -1).ToArray();
        }

        /// <summary>
        /// Executes an <see cref="System.Action"/> on <see cref="IEnumerable{T}"/>  items
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IEnumerable{T}"/> items </typeparam>
        /// <param name="enumerable">Source <see cref="IEnumerable{T}"/></param>
        /// <param name="Action"><see cref="System.Action"/> to execute</param>
        /// <param name="ExecuteAsParallel">If to execute in parallel</param>
        public static void ApplyToAll<T>(this IEnumerable<T> enumerable, System.Action<T> Action)
        {
            foreach (var item in enumerable)
            {
                Action(item);
            }
        }
    }
}
