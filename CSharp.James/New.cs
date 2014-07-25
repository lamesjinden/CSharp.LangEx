using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.James
{

    public static class New
    {

        public static T[] Array<T>(params T[] items)
        {
            return items;
        }

        public static IList<T> List<T>(params T[] items)
        {
            return new List<T>(items);
        }

        public static ISet<T> Set<T>(params T[] items)
        {
            return new HashSet<T>(items);
        }

        public static ISet<T> Set<T>(IEqualityComparer<T> comparer, params T[] items)
        {
            return new HashSet<T>(items, comparer);
        }

        public static IFluentEqualityComparerBuilder<T> ComparerOf<T>()
        {
            return new FluentEqualityComparerBuilder<T>();
        }

        public static IEqualityComparer<T> Comparer<T>(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            return new LambdaComparer<T>(equals, getHashCode);
        }

    }

}
