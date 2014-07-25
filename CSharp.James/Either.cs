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

        public Either(T value)
        {
            HasValue = true;
            Value = value;
        }

        public Either(Exception error)
        {
            HasError = true;
            Error = error;
        }

        public bool HasValue
        {
            get;
            private set;
        }

        public T Value
        {
            get;
            private set;
        }

        public bool HasError
        {
            get;
            private set;
        }

        public Exception Error
        {
            get;
            private set;
        }

        public static implicit operator Either<T>(T value)
        {           
            return new Either<T>(value);
        }

        public static implicit operator Either<T>(Exception exception)
        {
            return new Either<T>(exception);
        }

    }

}
