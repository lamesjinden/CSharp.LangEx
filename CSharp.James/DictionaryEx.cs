using System.Collections.Generic;

namespace PlayWell.Core
{

    public static class DictionaryEx
    {

        /// <summary>
        /// Adds an element with the provided key and value to <paramref name="source"/>
        /// and returns <paramref name="source"/>
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// This method does not check for potential key collisions.
        /// Attempting to add a duplicate key to <paramref name="source"/> will 
        /// throw an exception.
        /// </remarks>
        public static IDictionary<K, V> AddAnd<K, V>(this IDictionary<K, V> source, K key, V value)
        {
            source.Add(key, value);
            return source;
        }

        /// <summary>
        /// Adds an element with the provided key and value or updates 
        /// an existing element with the provided key and 
        /// returning <paramref name="source"/>
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IDictionary<K, V> UpsertAnd<K, V>(this IDictionary<K, V> source, K key, V value)
        {
            source[key] = value;
            return source;
        }

        /// <summary>
        /// Removes an element with the provided key 
        /// and returns <paramref name="source"/>
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IDictionary<K, V> RemoveAnd<K, V>(this IDictionary<K, V> source, K key, V value)
        {
            source.Remove(key);
            return source;
        }

        /// <summary>
        /// Gets the value associated with the provided key
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static V Get<K, V>(this IDictionary<K, V> source, K key)
        {
            return source[key];
        }

        /// <summary>
        /// Gets the value associated with the provided key or
        /// the default value of <typeparamref name="U"/>
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static V GetOrDefault<K, V>(this IDictionary<K, V> source, K key)
        {
            return source.ContainsKey(key) ? source[key] : default(V);
        }

        /// <summary>
        /// Gets the value associated with the provided
        /// key or <paramref name="fallback"/>
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public static V GetOrDefault<K, V>(this IDictionary<K, V> source, K key, V fallback)
        {
            return source.ContainsKey(key) ? source[key] : fallback;
        }

    }

}
