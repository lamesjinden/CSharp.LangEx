using System;

namespace PlayWell.Core
{

    public static class ExceptionEx
    {

        /// <summary>
        /// Creates and throws an instance of ArgumentNullException
        /// if and only if <paramref name="t"/> is a null reference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns>
        /// Returns <paramref name="t"/> if and only if 
        /// <paramref name="t"/> is not a null reference
        /// </returns>
        public static T ThrowIfNull<T>(this T t) where T : class
        {
            if (t == null) throw new ArgumentNullException();
            return t;
        }

        /// <summary>
        /// Creates and throws an instance of ArgumentNullException
        /// if and only if <paramref name="t"/> is a null reference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="paramName"></param>
        /// <returns>
        /// Returns <paramref name="t"/> if and only if 
        /// <paramref name="t"/> is not a null reference
        /// </returns>
        public static T ThrowIfNull<T>(this T t, string paramName) where T : class
        {
            if (t == null) throw new ArgumentNullException(paramName);
            return t;
        }

        /// <summary>
        /// Creates and throws an instance of ArgumentNullException
        /// if and only if <paramref name="t"/> is a null reference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        /// <returns>
        /// Returns <paramref name="t"/> if and only if 
        /// <paramref name="t"/> is not a null reference
        /// </returns>
        public static T ThrowIfNull<T>(this T t, string paramName, string message) where T : class
        {
            if (t == null) throw new ArgumentNullException(paramName, message);
            return t;
        }

    }

}
