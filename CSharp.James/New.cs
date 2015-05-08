using System;
using System.Collections.Generic;

namespace PlayWell.Core
{

    public static class New
    {

        /// <summary>
        /// Creates an array from the items in <paramref name="items"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static T[] Array<T>(params T[] items)
        {
            return items;
        }

        /// <summary>
        /// Creates an instance of IList of <typeparamref name="T"/> 
        /// from the items in <paramref name="items"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static List<T> List<T>(params T[] items)
        {
            return new List<T>(items);
        }

        /// <summary>
        /// Creates an instance of ISet of <typeparamref name="T"/>
        /// from the items in <paramref name="items"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static HashSet<T> Set<T>(params T[] items)
        {
            return new HashSet<T>(items);
        }

        /// <summary>
        /// Creates and instance of ISet of <typeparamref name="T"/>
        /// from the items in <paramref name="items"/> where item equality 
        /// is determined by <paramref name="comparer"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comparer"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static HashSet<T> Set<T>(IEqualityComparer<T> comparer, params T[] items)
        {
            return new HashSet<T>(items, comparer);
        }

        /// <summary>
        /// Creates and returns an instance of IFluentEqualityComparerBuilder of <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IFluentEqualityComparerBuilder<T> ComparerOf<T>()
        {
            return new FluentEqualityComparerBuilder<T>();
        }

        /// <summary>
        /// Creates and returns an IEqualityComparer of <typeparamref name="T"/>
        /// back by <paramref name="equals"/> and <paramref name="getHashCode"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="equals"></param>
        /// <param name="getHashCode"></param>
        /// <returns></returns>
        public static IEqualityComparer<T> Comparer<T>(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            return new LambdaComparer<T>(equals, getHashCode);
        }

    }

}
