using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.James
{

    /// <summary>
    /// Represents a value or a null reference
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Maybe<T> where T : class
    {

        /// <summary>
        /// Creates an instance of Maybe over <typeparamref name="T"/>
        /// </summary>
        /// <param name="value"></param>
        public Maybe(T value)
        {
            _hasValue = object.ReferenceEquals(null, value);
            _value = value; 
        }
        
        private readonly bool _hasValue;
        /// <summary>
        /// Gets a value indicating whether the current Maybe<T> object 
        /// has a valid value of its underlying type. 
        /// </summary>
        public bool HasValue 
        {
            get { return _hasValue; }
        }

        private readonly T _value;
        /// <summary>
        /// Gets the value of the current Maybe<T> object 
        /// if it has been assigned a valid underlying value.
        /// </summary>
        public T Value 
        { 
            get
            {
                if (!HasValue) throw new InvalidOperationException("The HasValue property is false.");
                return _value;
            }
        }

        /// <summary>
        /// Retrieves the value of the current Maybe<T> object, or the object's default value.
        /// </summary>
        /// <returns></returns>
        public T GetValueOrDefault()
        {
            return HasValue
                ? Value
                : default(T);
        }

        /// <summary>
        /// Retrieves the value of the current Maybe<T> object, or the specified default value.
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValueOrDefault(T defaultValue)
        {
            return HasValue
                ? Value
                : defaultValue;
        }

        /// <summary>
        /// Creates a new Maybe<T> object initialized to a specified value. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value);
        }

        /// <summary>
        /// Returns the value of a specified Maybe<T> value.
        /// </summary>
        /// <param name="maybe"></param>
        /// <returns></returns>
        public static explicit operator T(Maybe<T> maybe)
        {
            return maybe.Value;
        }

        public static readonly Maybe<T> None = new Maybe<T>();

    }

    public static class Maybe
    {

        /// <summary>
        /// Invokes <paramref name="selector"/> with the underlying value of <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Maybe<U> SelectMany<T, U>(this Maybe<T> source, Func<T, Maybe<U>> selector)
            where T : class where U : class
        {
            if (!source.HasValue) return Maybe<U>.None;
            return selector(source.Value);
        }

        //Projects each element of a sequence to an IEnumerable<T>, 
        //flattens the resulting sequences into one sequence, 
        //and invokes a result selector function on each element therein.

        /// <summary>
        /// Invokes <paramref name="selector"/> with the underlying value of <paramref name="source"/>,
        /// and invokes <paramref name="resultSelector"/> on the result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="maybeSelector"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        public static Maybe<V> SelectMany<T, U, V>(this Maybe<T> source, Func<T, Maybe<U>> maybeSelector, Func<T, U, V> resultSelector)
            where T : class where U : class where V : class
        {
            if (!source.HasValue) return Maybe<V>.None;

            var sourceUnwrapped = source.Value;
            var intermediate = maybeSelector(sourceUnwrapped);

            if (!intermediate.HasValue) return Maybe<V>.None;

            var interUnwrapped = intermediate.Value;
            var result = resultSelector(sourceUnwrapped, interUnwrapped);
            return result;
        }

    }

}
