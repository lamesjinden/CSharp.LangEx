using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LangEx
{
    public static class ExceptionEx
    {
        public static T ThrowIfNull<T>(this T t) where T : class
        {
            if (t == null) throw new ArgumentNullException();
            return t;
        }

        public static T ThrowIfNull<T>(this T t, string paramName) where T : class
        {
            if (t == null) throw new ArgumentNullException(paramName);
            return t;
        }

        public static T ThrowIfNull<T>(this T t, string paramName = null, string message = null) where T : class
        {
            if (t == null) throw new ArgumentNullException(paramName, message);
            return t;
        }
    }
}
