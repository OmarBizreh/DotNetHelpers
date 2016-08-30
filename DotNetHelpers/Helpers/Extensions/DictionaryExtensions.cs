using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetHelpers.Helpers.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Adds key, value to <see cref="Dictionary{TKey, TValue}"/> only if key is unique
        /// </summary>
        /// <typeparam name="TKey">Key to add</typeparam>
        /// <typeparam name="TValue">Value to add</typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool UniqueAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds value if key doesn't exist, else replaces value
        /// </summary>
        /// <typeparam name="TKey">Type of Key</typeparam>
        /// <typeparam name="TValue">Type of Value</typeparam>
        /// <param name="dictionary">Source Dictionary</param>
        /// <param name="key">Key to add or replace its value</param>
        /// <param name="value">Value to add</param>
        public static void AddOrReplace<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }

        /// <summary>
        /// Converts <see cref="IEnumerable{T}"/> to <see cref="Dictionary{TKey, TValue}"/> and ignores dublicated keys
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IEnumerable{T}"/> items</typeparam>
        /// <typeparam name="TKey">Type of <see cref="Dictionary{TKey, TValue}"/> Keys</typeparam>
        /// <typeparam name="TValue">Type of <see cref="Dictionary{TKey, TValue}"/> Values</typeparam>
        /// <param name="collection">Collection to convert</param>
        /// <param name="keySelector"><see cref="Func{T, TResult}"/> to get key for collection item </param>
        /// <param name="valueSelector"><see cref="Func{T, TResult}"/> to get valu for collection item </param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> ToDictionary<T, TKey, TValue>(this IEnumerable<T> collection, Func<T, TKey> keySelector, Func<T, TValue> valueSelector)
        {
            var mResultDictionary = new Dictionary<TKey, TValue>();
            foreach (var item in collection)
            {
                var key = keySelector(item);
                if (!mResultDictionary.ContainsKey(key))
                    mResultDictionary.Add(key, valueSelector(item));
            }
            return mResultDictionary;
        }
    }
}