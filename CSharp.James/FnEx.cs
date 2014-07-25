using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.James
{

    public static class FnEx
    {

        public static void Apply<T>(this T arg, Action<T> action)
        {
            action(arg);
        }

        public static U Apply<T, U>(this T arg, Func<T,U > func)
        {
            return func(arg);
        }

        public static void ApplyIf<T>(this T arg, bool guard, Action<T> action)
        {
            if (guard) action(arg);
        }

        public static void ApplyIf<T>(this T arg, Func<T, bool> guard, Action<T> action)
        {
            if (guard(arg)) action(arg);
        }

        public static U ApplyIf<T, U>(this T arg, bool guard, Func<T, U> func)
        {
            return guard ? func(arg) : default(U);
        }

        public static U ApplyIf<T, U>(this T arg, Func<T, bool> guard, Func<T, U> func)
        {
            return guard(arg) ? func(arg) : default(U);
        }

        public static U ApplyIf<T, U>(this T arg, bool guard, Func<T, U> func, U fallback)
        {
            return guard ? func(arg) : fallback;
        }

        public static U ApplyIf<T, U>(this T arg, Func<T, bool> guard, Func<T, U> func, U fallback)
        {
            return guard(arg) ? func(arg) : fallback;
        }

        public static void IfNotNull<T>(this T arg, Action<T> action) where T : class
        {
            arg.ApplyIf(arg != null, action);
        }

        public static U IfNotNull<T, U>(this T arg, Func<T, U> func) where T : class 
        {
            return arg.ApplyIf(arg != null, func);
        }

        public static U IfNotNull<T, U>(this T arg, Func<T, U> func, U fallback) where T : class
        {
            return arg.ApplyIf(arg != null, func, fallback);
        }

        public static T Return<T>(this T arg, Action action)
        {
            action();
            return arg;
        }

        public static T Return<T>(this T arg, Action<T> action)
        {
            action(arg);
            return arg;
        }

        public static Func<T, V> Compose<T,  U,V>(this Func<U, V> f, Func<T, U> g)
        {
            return x => f(g(x));
        }

        public static Func<T, Func<U, V>> Curry<T, U, V>(this Func<T, U, V> f)
        {
            return x => y => f(x, y);
        }

        public static Func<T, Func<U, Func<V, W>>> Curry<T, U, V, W>(this Func<T, U, V, W> f)
        {
            return x => y => z => f(x, y, z);
        }

        public static Func<U, V> PartialL<T, U, V>(this Func<T, U, V> f, T arg)
        {
            return x => f(arg, x);
        }

        public static Func<T, V> PartialR<T, U, V>(this Func<T, U, V> f, U arg)
        {
            return x => f(x, arg);
        }
       
        public static Func<U, V, W> PartialL<T, U, V, W>(this Func<T, U, V, W> f, T arg)
        {
            return (x, y) => f(arg, x, y);
        }

        public static Func<T, U, W> PartialR<T, U, V, W>(this Func<T, U, V, W> f, V arg)
        {
            return (x, y) => f(x, y, arg);
        }

        public static Func<V, W> PartialL<T, U, V, W>(this Func<T, U, V, W> f, T arg1, U arg2)
        {
            return x => f(arg1, arg2, x);
        }

        public static Func<T, W> PartialR<T, U, V, W>(this Func<T, U, V, W> f, U arg1, V arg2)
        {
            return x => f(x, arg1, arg2);
        }

    }

}
