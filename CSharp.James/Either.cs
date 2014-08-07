using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.James
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

}
