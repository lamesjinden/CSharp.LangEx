using System;

namespace PlayWell.Core
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
        public static string OrEmpty(this string str)
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

        /// <summary>
        /// Invokes Bool.TryParse, returning the value as Nullable of bool
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool? ToBool(this string value)
        {
            bool parsed;
            return bool.TryParse(value, out parsed)
                ? parsed
                : (bool?)null;
        }

        /// <summary>
        /// Invokes Int.TryParse, returning the value as Nullable of int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int? ToInt(this string value)
        {
            int parsed;
            return int.TryParse(value, out parsed)
                ? parsed
                : (int?)null;
        }

        /// <summary>
        /// Invokes Long.TryParse, returning the value as Nullable of long
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long? ToLong(this string value)
        {
            long parsed;
            return long.TryParse(value, out parsed)
                ? parsed
                : (long?)null;
        }

        /// <summary>
        /// Invokes Guid.TryParse, returning the value as Nullable of Guid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid? ToGuid(this string value)
        {
            Guid parsed;
            return Guid.TryParse(value, out parsed)
                ? parsed
                : (Guid?)null;
        }

    }

}
