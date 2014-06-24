using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LangEx
{
    public class Either<T>
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
    }
}
