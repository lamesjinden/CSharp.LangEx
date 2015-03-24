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
    public struct Maybe<T> : IEquatable<Maybe<T>> where T : class
    {

        public static readonly Maybe<T> None = new Maybe<T>();

        /// <summary>
        /// Creates an instance of Maybe over <typeparamref name="T"/>
        /// </summary>
        /// <param name="value"></param>
        public Maybe(T value)
        {
            _hasValue = !object.ReferenceEquals(null, value);
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

        /// <summary>
        /// Determines equality of this instance and <paramref name="other"/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Maybe<T> other)
        {
            return  HasValue && other.HasValue
                ? EqualityComparer<T>.Default.Equals(Value, other.Value)
                : false;
        }
        
        /// <summary>
        /// Determines equality of this instance and <paramref name="obj"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            return obj is Maybe<T> && Equals((Maybe<T>)obj);            
        }
        
        /// <summary>
        /// Operator overload - mapped to Equals
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Maybe<T> left, Maybe<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Operator overload - mapped to negation of Equals
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Maybe<T> left, Maybe<T> right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Not Implemented. Throws NotSupportedException
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException">Always</exception>
        public override int GetHashCode()
        {
            //when would this make sense?
            throw new NotSupportedException();
        }

    }

    public static class Maybe
    {

        /// <summary>
        /// Monadic Bind for Maybe
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="maybe"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public static Maybe<U> Bind<T, U>(this Maybe<T> maybe, Func<T, Maybe<U>> project)
            where T : class
            where U : class
        {
            return maybe.HasValue
                ? project(maybe.Value)
                : Maybe<U>.None;
        }

        /// <summary>
        /// If present, projects the value of <paramref name="maybe"/> into a new instance of Maybe.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="maybe"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Maybe<U> Select<T, U>(this Maybe<T> maybe, Func<T, U> selector) 
            where T : class 
            where U : class
        {
            return Bind(maybe, AdaptSelector(selector));
        }
         
        /// <summary>
        /// Adapt <paramref name="selector"/> to projector of Bind
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static Func<T, Maybe<U>> AdaptSelector<T, U>(Func<T, U> selector)
            where T : class
            where U : class
        {
            return t => selector(t);
        }

        /// <summary>
        /// If present, filters the value of <paramref name="maybe"/> based on a predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maybe"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Maybe<T> Where<T>(this Maybe<T> maybe, Func<T, bool> predicate) 
            where T : class
        {
            return Bind(maybe, t => predicate(t) ? maybe : Maybe<T>.None);
        }

        /// <summary>
        /// Alias of Bind
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Maybe<U> SelectMany<T, U>(this Maybe<T> source, Func<T, Maybe<U>> selector)
            where T : class
            where U : class 
        {
            return Bind(source, selector);
        }

        /// <summary>
        /// If present, projects the value of <paramref name="source"/>. 
        /// If the result represents a value, it is projected via <paramref name="resultSelector"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="maybeSelector"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        public static Maybe<V> SelectMany<T, U, V>(this Maybe<T> source, Func<T, Maybe<U>> maybeSelector, Func<T, U, V> resultSelector)
            where T : class 
            where U : class 
            where V : class
        {
            var maybeInter = Bind(source, maybeSelector);
            return Bind(maybeInter, AdaptResultSelector(resultSelector, source));
        }

        /// <summary>
        /// Adapt <paramref name="resultSelector"/> to signature of projector of Bind
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="resultSelector"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private static Func<U, Maybe<V>> AdaptResultSelector<T, U, V>(Func<T, U, V> resultSelector, Maybe<T> source)
            where T : class
            where U : class
            where V : class
        {
            return u => resultSelector(source.Value, u);
        }

        /// <summary>
        /// Invokes <paramref name="success"/> if maybe represents a non-null reference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maybe"></param>
        /// <param name="success"></param>
        public static void Do<T>(this Maybe<T> maybe, Action<T> success)
            where T : class
        {
            if (maybe.HasValue) success(maybe.Value);
        }

        /// <summary>
        /// If <paramref name="maybe"/> is non-empty, invokes <paramref name="success"/>; 
        /// otherwise invokes <paramref name="fail"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maybe"></param>
        /// <param name="success"></param>
        /// <param name="fail"></param>
        public static void Do<T>(this Maybe<T> maybe, Action<T> success, Action fail)
            where T : class
        {
            if (maybe.HasValue) success(maybe.Value);
            else fail();
        }

        /// <summary>
        /// If present, returns the value of <paramref name="maybe"/>; otherwise <paramref name="fallback"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maybe"></param>
        /// <param name="else"></param>
        /// <returns></returns>
        public static T IfNotNone<T>(this Maybe<T> maybe, T @else)
            where T : class
        {
            return maybe.HasValue
                ? maybe.Value
                : @else;
        }

        /// <summary>
        /// If <paramref name="maybe"/> is non-empty, returns the result of invoking <paramref name="project"/> with the value;
        /// otherwise fallback
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="maybe"></param>
        /// <param name="project"></param>
        /// <param name="else"></param>
        /// <returns></returns>
        public static U IfNotNone<T, U>(this Maybe<T> maybe, Func<T, U> project, U @else)
            where T : class
        {
            return maybe.HasValue
                ? project(maybe.Value)
                : @else;
        }

        /// <summary>
        /// Wraps <paramref name="value"/> in an instance of Maybe
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Maybe<T> AsMaybe<T>(this T value)
            where T : class
        {
            return value;
        }

        /// <summary>
        /// Returns the first element of <paramref name="sequence"/> matching <paramref name="predicate"/> 
        /// as an instance of Maybe. If no such element exists, Maybe.None.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Maybe<T> FirstAsMaybe<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
            where T : class
        {
            return sequence.FirstOrDefault(predicate);
        }

    }

}
