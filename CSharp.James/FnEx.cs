using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.James
{

    public static class FnEx
    {

        /// <summary>
        /// Returns a Func of <typeparamref name="T"/> that returns <paramref name="arg"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Func<T> Wrap<T>(this T arg)
        {
            return () => arg;
        }

        /// <summary>
        /// Applies <paramref name="action"/> to <paramref name="arg"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        /// <param name="action"></param>
        public static void Apply<T>(this T arg, Action<T> action)
        {
            action(arg);
        }

        /// <summary>
        /// Applies <paramref name="func"/> to <paramref name="arg"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="arg"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static U Apply<T, U>(this T arg, Func<T,U> func)
        {
            return func(arg);
        }

        /// <summary>
        /// Conditionally applies <paramref name="action"/> 
        /// to <paramref name="arg"/> if and only if <paramref name="guard"/> is true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        /// <param name="guard"></param>
        /// <param name="action"></param>
        public static void ApplyIf<T>(this T arg, bool guard, Action<T> action)
        {
            if (guard) action(arg);
        }

        /// <summary>
        /// Conditionally applies <paramref name="action"/> 
        /// to <paramref name="T"/> if and only if the result of 
        /// applying <paramref name="guard"/> to <paramref name="arg"/> is true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        /// <param name="guard"></param>
        /// <param name="action"></param>
        public static void ApplyIf<T>(this T arg, Func<T, bool> guard, Action<T> action)
        {
            if (guard(arg)) action(arg);
        }

        /// <summary>
        /// Conditionally applies <paramref name="func"/> to <paramref name="arg"/>
        /// if and only if <paramref name="guard"/> is true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="arg"></param>
        /// <param name="guard"></param>
        /// <param name="func"></param>
        /// <returns>
        /// The result of applying <paramref name="func"/> to <paramref name="arg"/>
        /// if and only if <paramref name="guard"/> is true; else the default
        /// value of <typeparamref name="U"/>
        /// </returns>
        public static U ApplyIf<T, U>(this T arg, bool guard, Func<T, U> func)
        {
            return guard ? func(arg) : default(U);
        }

        /// <summary>
        /// Conditionally applies <paramref name="func"/> to <paramref name="arg"/>
        /// if and only if the result of applying <paramref name="guard"/> to <paramref name="arg"/>
        /// is true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="arg"></param>
        /// <param name="guard"></param>
        /// <param name="func"></param>
        /// <returns>
        /// The result of applying <paramref name="func"/> to <paramref name="arg"/>
        /// if and only if the result of applying <paramref name="guard"/> to <paramref name="arg"/>
        /// is true; else the default value of <typeparamref name="U"/>
        /// </returns>
        public static U ApplyIf<T, U>(this T arg, Func<T, bool> guard, Func<T, U> func)
        {
            return guard(arg) ? func(arg) : default(U);
        }

        /// <summary>
        /// Conditionally applies <paramref name="func"/> to <paramref name="arg"/>
        /// if and only if <paramref name="guard"/> is true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="arg"></param>
        /// <param name="guard"></param>
        /// <param name="func"></param>
        /// <param name="fallback"></param>
        /// <returns>
        /// The result of applying <paramref name="func"/> to <paramref name="arg"/>
        /// if and only if <paramref name="guard"/> is true; else <paramref name="fallback"/>
        /// </returns>
        public static U ApplyIf<T, U>(this T arg, bool guard, Func<T, U> func, U fallback)
        {
            return guard ? func(arg) : fallback;
        }

        /// <summary>
        /// Conditionally applies <paramref name="func"/> to <paramref name="arg"/>
        /// if and only if the result of applying <paramref name="guard"/> to <paramref name="arg"/>
        /// is true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="arg"></param>
        /// <param name="guard"></param>
        /// <param name="func"></param>
        /// <param name="fallback"></param>
        /// <returns>
        /// The result of applying <paramref name="func"/> to <paramref name="arg"/>
        /// if and only if the result of applying <paramref name="guard"/> to <paramref name="arg"/>
        /// is true; else <paramref name="fallback"/>
        /// </returns>
        public static U ApplyIf<T, U>(this T arg, Func<T, bool> guard, Func<T, U> func, U fallback)
        {
            return guard(arg) ? func(arg) : fallback;
        }

        /// <summary>
        /// Applies <paramref name="action"/> to <paramref name="arg"/>
        /// if and only if <paramref name="arg"/> is not a null reference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        /// <param name="action"></param>
        public static void IfNotNull<T>(this T arg, Action<T> action) where T : class
        {
            arg.ApplyIf(arg != null, action);
        }

        /// <summary>
        /// Applies <paramref name="func"/> to <paramref name="arg"/>
        /// if and only if <paramref name="arg"/> is not a null reference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="arg"></param>
        /// <param name="func"></param>
        /// <returns>
        /// The result of applying <paramref name="func"/> to <paramref name="arg"/>
        /// if and only if <paramref name="arg"/> is not a null reference;
        /// else the default value for <paramref name="U"/>
        /// </returns>
        public static U IfNotNull<T, U>(this T arg, Func<T, U> func) where T : class 
        {
            return arg.ApplyIf(arg != null, func);
        }

        /// <summary>
        /// Applies <paramref name="func"/> to <paramref name="arg"/>
        /// if and only if <paramref name="arg"/> is not a null reference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="arg"></param>
        /// <param name="func"></param>
        /// <param name="fallback"></param>
        /// <returns>
        /// The result of applying <paramref name="func"/> to <paramref name="arg"/>
        /// if and only if <paramref name="arg"/> is not a null reference;
        /// else <paramref name="fallback"/>
        /// </returns>
        public static U IfNotNull<T, U>(this T arg, Func<T, U> func, U fallback) where T : class
        {
            return arg.ApplyIf(arg != null, func, fallback);
        }

        /// <summary>
        /// Applies <paramref name="action"/> and returns <paramref name="arg"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Return<T>(this T arg, Action action)
        {
            action();
            return arg;
        }

        /// <summary>
        /// Applies <paramref name="action"/> to <paramref name="arg"/>
        /// and returns <paramref name="arg"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Return<T>(this T arg, Action<T> action)
        {
            action(arg);
            return arg;
        }

        /// <summary>
        /// Creates and returns a Func of <typeparamref name="U"/>
        /// by composing <paramref name="f"/> and <paramref name="g"/>
        /// such that the result is equivalent to f(g())
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="g"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Func<U> Comp<T, U>(this Func<T> g, Func<T, U> f)
        {
            return () => f(g());
        }

        /// <summary>
        /// Creates and returns an Action of <typeparamref name="T"/> by 
        /// composing <paramref name="f"/> and <paramref name="g"/>
        /// such that result is equivalent to f(g(x))
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="g"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Action<T> Comp<T, U>(this Func<T, U> g, Action<U> f)
        {
            return t => f(g(t));
        }

        /// <summary>
        /// Creates and returns a Func of <typeparamref name="T"/> to <typeparamref name="V"/>
        /// by composing <paramref name="f"/> and <paramref name="g"/> such that the result
        /// is equivalenet to f(g(x))
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="g"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Func<T, V> Comp<T, U, V>(this Func<T, U> g, Func<U, V> f)
        {
            return x => f(g(x));
        }

        /// <summary>
        /// Creates and returns a Func of <typeparamref name="T"/> to <typeparamref name="W"/>
        /// by composing <paramref name="f"/>, <paramref name="g"/>, and <paramref name="h"/>
        /// such that the result is equivalent to f(g(h(x)))
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="h"></param>
        /// <param name="g"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Func<T, W> Comp<T, U, V, W>(Func<T, U> h, Func<U, V> g, Func<V, W> f)
        {
            return x => f(g(h(x)));
        }

        /// <summary>
        /// Creates and returns a Func of <typeparamref name="T"/> to <typeparamref name="T"/>
        /// by composing the elements of <paramref name="funcs"/> where the last element 
        /// of <paramref name="funcs"/> is the inner-most function in the composition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcs"></param>
        /// <returns></returns>
        public static Func<T, T> Comp<T>(params Func<T, T>[] funcs)
        {
            return funcs.Aggregate((outer, inner) => x => outer(inner(x)));
        }

        /// <summary>
        /// Creates and returns a Func of <typeparamref name="T"/> to <typeparamref name="T"/>
        /// by composing the elements of <paramref name="funcs"/> where the last element 
        /// of <paramref name="funcs"/> is the inner-most function in the composition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcs"></param>
        /// <returns></returns>
        public static Func<T, T> Comp<T>(IEnumerable<Func<T, T>> funcs)
        {
            return Comp(funcs.ToArray());
        }

        /// <summary>
        /// Creates and returns the result of currying <paramref name="f"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Func<T, Func<U, V>> Curry<T, U, V>(this Func<T, U, V> f)
        {
            return x => y => f(x, y);
        }

        /// <summary>
        /// Creates and returns the result of currying <paramref name="f"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Func<T, Func<U, Func<V, W>>> Curry<T, U, V, W>(this Func<T, U, V, W> f)
        {
            return x => y => z => f(x, y, z);
        }

        /// <summary>
        /// Partially applies <paramref name="f"/> with argument <paramref name="arg"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Func<U> Partial<T, U>(this Func<T, U> f, T arg)
        {
            return () => f(arg);
        }

        /// <summary>
        /// Partially applies <paramref name="f"/> with
        /// arguments <paramref name="arg1"/> and <paramref name="arg2"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public static Func<V> Partial<T, U, V>(this Func<T, U, V> f, T arg1, U arg2)
        {
            return () => f(arg1, arg2);
        }

        /// <summary>
        /// Partially applies the leftmost argument of <paramref name="f"/>
        /// with <paramref name="arg"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Func<U, V> PartialL<T, U, V>(this Func<T, U, V> f, T arg)
        {
            return x => f(arg, x);
        }

        /// <summary>
        /// Partially applies the rightmost argument of <paramref name="f"/>
        /// with <paramref name="arg"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Func<T, V> PartialR<T, U, V>(this Func<T, U, V> f, U arg)
        {
            return x => f(x, arg);
        }
       
        /// <summary>
        /// Partially apaplies the leftmost argument of <paramref name="f"/>
        /// with <paramref name="arg"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Func<U, V, W> Partial<T, U, V, W>(this Func<T, U, V, W> f, T arg)
        {
            return (x, y) => f(arg, x, y);
        }
        
        /// <summary>
        /// Partially applies the rightmost argument of <paramref name="f"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Func<T, U, W> PartialR<T, U, V, W>(this Func<T, U, V, W> f, V arg)
        {
            return (x, y) => f(x, y, arg);
        }

        /// <summary>
        /// Partially applies the two leftmost arguments of <paramref name="f"/>
        /// with <paramref name="arg1"/> and <paramref name="arg2"/> respectively
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public static Func<V, W> Partial<T, U, V, W>(this Func<T, U, V, W> f, T arg1, U arg2)
        {
            return x => f(arg1, arg2, x);
        }

        /// <summary>
        /// Partially applies the two rightmost arguments of <paramref name="f"/>
        /// with <paramref name="arg1"/> and <paramref name="arg2"/> respectively
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public static Func<T, W> PartialR<T, U, V, W>(this Func<T, U, V, W> f, U arg1, V arg2)
        {
            return x => f(x, arg1, arg2);
        }

        /// <summary>
        /// Partially applies <paramref name="f"/> with argument <paramref name="arg"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Action Partial<T>(this Action<T> f, T arg)
        {
            return () => f(arg);
        }

        /// <summary>
        /// Partially applies <paramref name="f"/> with argument <paramref name="arg"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Action<U> Partial<T, U>(this Action<T, U> f, T arg)
        {
            return u => f(arg, u);
        }

        /// <summary>
        /// Partially applies the right-most argument of <paramref name="f"/> with argument <paramref name="arg"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Action<T> PartialR<T, U>(this Action<T, U> f, U arg)
        {
            return t => f(t, arg);
        }

        /// <summary>
        /// Partially applies <paramref name="f"/> with argument <paramref name="arg"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Action<U, V> Partial<T, U, V>(this Action<T, U, V> f, T arg)
        {
            return (u, v) => f(arg, u, v);
        }

        /// <summary>
        /// Partially applies the rightmost argument of <paramref name="f"/> with argument <paramref name="arg"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="f"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Action<T, U> PartialR<T, U, V>(this Action<T, U, V> f, V arg)
        {
            return (t, u) => f(t, u, arg);
        }

    }

}
