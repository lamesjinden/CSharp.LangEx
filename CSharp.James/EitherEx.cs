using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.James
{

    public static class EitherEx
    {

        /// <summary>
        /// Returns the result of applying <paramref name="selector"/> to the Value
        /// of <paramref name="source"/> if and only if <paramref name="source"/>
        /// does not represent an error; else an instance of Either representing 
        /// the error of <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Either<U> SelectMany<T, U>(this Either<T> source, Func<T, Either<U>> selector)
        {
            if (source.HasError) return source.Error;
            return selector(source.Value);
        }

        /// <summary>
        /// Whatever SelectMany does
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="eitherSelector"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        public static Either<V> SelectMany<T, U, V>(this Either<T> source, Func<T, Either<U>> eitherSelector, Func<T, U, V> resultSelector)
        {
            if (source.HasError) return source.Error;

            var sourceUnwrapped = source.Value;
            var intermediate = eitherSelector(sourceUnwrapped);

            if (intermediate.HasError) return intermediate.Error;

            var interUnwrapped = intermediate.Value;
            var result = resultSelector(sourceUnwrapped, interUnwrapped);
            return result;
        }

        /// <summary>
        /// Applies <paramref name="f"/> and projects the result to Either
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Either<T> Try<T>(Func<T> f)
        {
            return f.Tries()();
        }

        /// <summary>
        /// Creates and returns a Func that projects
        /// the result of applying <paramref name="f"/> to Either
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Func<Either<T>> Tries<T>(this Func<T> f)
        {
            return () =>
            {
                try
                {
                    return f();
                }
                catch (Exception exception)
                {
                    return exception;
                }
            };
        }

        /// <summary>
        /// Creates and returns a Func that projects
        /// the result of applying <paramref name="f"/> to Either
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Func<T, Either<U>> Tries<T, U>(this Func<T, U> f)
        {
            return x =>
            {
                try
                {
                    return f(x);
                }
                catch (Exception exception)
                {
                    return exception;
                }
            };
        }

        /// <summary>
        /// Creates and returns a Func that projects
        /// the result of applying <paramref name="f"/> to Either
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Func<T, U, Either<V>> Tries<T, U, V>(this Func<T, U, V> f)
        {
            return (x, y) =>
            {
                try
                {
                    return f(x, y);
                }
                catch (Exception exception)
                {
                    return exception;
                }
            };
        }

        public static void IfSuccess<T>(this Either<T> either, Action<T> action)
        {
            if (either.HasValue) action(either.Value);
        }

        public static U IfSuccess<T, U>(this Either<T> either, Func<T, U> func)
        {
            if (either.HasValue) return func(either.Value);
            return default(U);
        }

        public static U IfSuccess<T, U>(this Either<T> either, Func<T, U> func, U fallback)
        {
            if (either.HasValue) return func(either.Value);
            return fallback;
        }

        public static void IfError<T>(this Either<T> either, Action<Exception> action)
        {
            if (either.HasError) action(either.Error);
        }

        public static U IfError<T, U>(this Either<T> either, Func<Exception, U> func)
        {
            if (either.HasError) return func(either.Error);
            return default(U);
        }

        public static U IfError<T, U>(this Either<T> either, Func<Exception, U> func, U fallback)
        {
            if (either.HasError) return func(either.Error);
            return fallback;
        }

    }

}
