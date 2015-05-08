using System;

namespace PlayWell.Core
{

    /// <summary>
    /// Represents a value or error (as instance of Exception)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Either<T>
    {

        /// <summary>
        /// Creates an instance of Either
        /// </summary>
        /// <param name="value"></param>
        public Either(T value)
        {
            HasValue = true;
            Value = value;
        }

        /// <summary>
        /// Creates an instance of Either
        /// </summary>
        /// <param name="error"></param>
        public Either(Exception error)
        {
            HasError = true;
            Error = error;
        }

        /// <summary>
        /// Gets a value indicating whether the current Either has a value
        /// </summary>
        public bool HasValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value of the current Either
        /// </summary>
        public T Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the curent Either has an error
        /// </summary>
        public bool HasError
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Exception representing the error of the current Either
        /// </summary>
        public Exception Error
        {
            get;
            private set;
        }

        /// <summary>
        /// Implicit cast operator for Either
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Either<T>(T value)
        {            
            return new Either<T>(value);
        }

        /// <summary>
        /// Implicit cast operator for Either
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static implicit operator Either<T>(Exception exception)
        {
            return new Either<T>(exception);
        }

    }

    public static class Either
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
        public static Either<U> FlatMap<T, U>(this Either<T> source, Func<T, Either<U>> selector)
        {
            if (source.HasError) return source.Error;
            return selector(source.Value);
        }

        /// <summary>
        /// Whatever FlatMap does
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="eitherSelector"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        public static Either<V> FlatMap<T, U, V>(this Either<T> source, Func<T, Either<U>> eitherSelector, Func<T, U, V> resultSelector)
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
        public static Func<T, Either<T>> Tries<T>(this Func<T, T> f)
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

    }

}
