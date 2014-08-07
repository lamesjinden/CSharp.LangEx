using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.James
{

    public static class StringEx
    {

        /// <summary>
        /// Returns <paramref name="str"/> if and only 
        /// if <paramref name="str"/> is not a null reference; 
        /// else the empty string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Maybe(this string str)
        {
            return str ?? string.Empty;
        }

        /// <summary>
        /// Creates and throws an instance of ArgumentException
        /// if and only if <paramref name="str"/> is a null reference
        /// or entirely white-space characters; else <paramref name="str"/>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ThrowIfNullOrWhiteSpace(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentException();
            return str;
        }

        /// <summary>
        /// Creates and throws an instance of ArgumentException
        /// if and only if <paramref name="str"/> is a null reference
        /// or entirely white-space characters; else <paramref name="str"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="message"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static string ThrowIfNullOrWhiteSpace(this string str, string message = null, string paramName = null)
        {
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentException(message, paramName);
            return str;
        }

    }

}
