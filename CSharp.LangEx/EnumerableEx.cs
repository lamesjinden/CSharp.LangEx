using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LangEx
{
    public static class EnumerableEx
    {
        public static IEnumerable<T> Maybe<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        public static IEnumerable<T> Tap<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<T> WhereAll<T>(this IEnumerable<T> source, params Func<T,bool>[] predicates)
        {
            foreach (var item in source)
            {
                if (predicates.All(p => p(item)))
                    yield return item;               
            }
        }

        public static IEnumerable<T> WhereAny<T>(this IEnumerable<T> source, params Func<T,bool>[] predicates)
        {
            foreach (var item in source)
            {
                if (predicates.Any(p => p(item)))
                    yield return item;
            }
        }
    }
}
