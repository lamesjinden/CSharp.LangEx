using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.James
{

    public static class StructEx
    {

        /// <summary>
        /// Returns <paramref name="value"/> as Nullable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T? Nullify<T>(this T value)
            where T : struct
        {
            return value;
        }

        /// <summary>
        /// Returns a Func that returns the Nullable result 
        /// of applying <paramref name="func"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Func<T?> Nullify<T>(this Func<T> func)
            where T : struct
        {
            return () => func();
        }

        /// <summary>
        /// Returns a Func that returns the Nullable result
        /// of applying <paramref name="func"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Func<T, T?> Nullify<T>(this Func<T, T> func)
            where T : struct
        {
            return x => func(x);
        }

        /// <summary>
        /// Returns a Func that returns the Nullable result
        /// of applying <paramref name="func"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Func<T, U?> Nullify<T, U>(this Func<T, U> func)
            where U : struct
        {
            return x => func(x);
        }

        public static Nullable<U> Select<T, U>(this Nullable<T> nullable, Func<T, U> selector)
            where T : struct 
            where U : struct
        {
            return nullable.HasValue
                ? selector(nullable.Value)
                : null as U?;
        }

        public static Nullable<T> Where<T>(this Nullable<T> nullable, Func<T, bool> predicate)
            where T : struct
        {
            return nullable.HasValue && predicate(nullable.Value)
                ? nullable
                : null as T?;
        }

        public static Nullable<V> SelectMany<T, U, V>(this Nullable<T> source, Func<T, Nullable<U>> nullableSelector, Func<T, U, V> resultSelector)
            where T : struct
            where U : struct
            where V : struct
        {
            if (!source.HasValue) return null as V?;

            var sourceUnwrapped = source.Value;
            var intermediate = nullableSelector(sourceUnwrapped);

            if (!intermediate.HasValue) return null as V?;

            var interUnwrapped = intermediate.Value;
            var result = resultSelector(sourceUnwrapped, interUnwrapped);
            return result;
        }

    }

}
