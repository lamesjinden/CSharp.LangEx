using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.James
{

    public static class EnumerableEx
    {

        /// <summary>
        /// Returns <paramref name="source"/> or, if null, Enumerable.Empty of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> Maybe<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Invokes <paramref name="action"/> on each item of <paramref name="source"/>
        /// and returns the item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> Tap<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }

        /// <summary>
        /// Filters <paramref name="source"/>, including only items that pass 
        /// every predicate in <paramref name="predicates"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereAll<T>(this IEnumerable<T> source, params Func<T, bool>[] predicates)
        {
            foreach (var item in source)
            {
                if (predicates.All(p => p(item)))
                    yield return item;               
            }
        }

        /// <summary>
        /// Filters <paramref name="source"/>, including only items that pass
        /// at least one predicate in <paramref name="predicates"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereAny<T>(this IEnumerable<T> source, params Func<T, bool>[] predicates)
        {
            foreach (var item in source)
            {
                if (predicates.Any(p => p(item)))
                    yield return item;
            }
        }

        /// <summary>
        /// Returns a IEnumerable of T that has been filtered for each 
        /// predicate in <paramref name="predicates"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereMany<T>(this IEnumerable<T> source, params Func<T, bool>[] predicates)
        {
            return predicates.Aggregate(source, (seq, pred) => seq.Where(pred));
        }

        /// <summary>
        /// Concatenates a single instance onto <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T item)
        {
            return source.Concat(new[] { item });
        }

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
        public static IDictionary<K, V> AddAnd<K, V>(this IDictionary<K,V> source, K key, V value)
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
