using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LangEx
{
    public static class EitherEx
    {
        public static Either<U> SelectMany<T,U>(this Either<T> source, Func<T, Either<U>> selector)
        {
            if (source.HasError) return new Either<U>(source.Error);          
            return selector(source.Value);
        }

        public static Either<V> SelectMany<T,U,V>(this Either<T> source, Func<T, Either<U>> eitherSelector, Func<T,U,V> resultSelector)
        {
            if (source.HasError) return new Either<V>(source.Error);

            var sourceUnwrapped = source.Value;
            var intermediate = eitherSelector(sourceUnwrapped);

            if (intermediate.HasError) return new Either<V>(intermediate.Error);

            var interUnwrapped = intermediate.Value;
            var result = resultSelector(sourceUnwrapped, interUnwrapped);
            return new Either<V>(result);
        }

        public static Either<T> Catch<T>(Func<T> f)
        {
            return f.ToEither()();
        }

        public static Func<Either<T>> ToEither<T>(this Func<T> f)
        {
            return () =>
                {
                    try
                    {
                        return new Either<T>(f());
                    }
                    catch (Exception exception)
                    {
                        return new Either<T>(exception);
                    }
                };
        }

        public static Func<T, Either<U>> ToEither<T,U>(this Func<T,U> f)
        {
            return x =>
                {
                    try
                    {
                        return new Either<U>(f(x));
                    }
                    catch (Exception exception)
                    {
                        return new Either<U>(exception);
                    }
                };
        }

        public static Func<T, U, Either<V>> ToEither<T,U,V>(this Func<T,U,V> f)
        {
            return (x,y) =>
            {
                try
                {
                    return new Either<V>(f(x,y));
                }
                catch (Exception exception)
                {
                    return new Either<V>(exception);
                }
            };
        }

        public static void IfNotError<T>(this Either<T> either, Action<T> action)
        {
            if (either.HasError) return;
            action(either.Value);
        }

        public static U IfNotError<T,U>(this Either<T> either, Func<T,U> func)
        {
            if (either.HasError) return default(U);
            return func(either.Value);
        }

        public static U IfNotError<T,U>(this Either<T> either, Func<T,U> func, U fallback)
        {
            if (either.HasError) return fallback;
            return func(either.Value);
        }
    }
}
