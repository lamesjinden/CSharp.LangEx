using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LangEx
{
    public static class StringEx
    {
        public static string Maybe(this string str)
        {
            return str ?? string.Empty;
        }

        public static string ThrowIfNullOrWhiteSpace(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentException();
            return str;
        }

        public static string ThrowIfNullOrWhiteSpace(this string str, string message = null, string paramName = null)
        {
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentException(message, paramName);
            return str;
        }
    }
}
