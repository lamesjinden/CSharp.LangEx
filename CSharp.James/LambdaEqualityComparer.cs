using System;
using System.Collections.Generic;

namespace PlayWell.Core
{

    public interface IFluentEqualityComparerBuilder<T> 
    {
        IEqualityComparer<T> On<U>(Func<T, U> selector);    
    }

    public class FluentEqualityComparerBuilder<T> : IFluentEqualityComparerBuilder<T>
    {
        public IEqualityComparer<T> Build<U>(Func<T, U> selector)
        {
            return new LambdaComparer<T>(
                (a, b) => selector(a).Equals(selector(b)),
                (a) => selector(a).GetHashCode());
        }

        public IEqualityComparer<T> On<U>(Func<T, U> selector)
        {
            return Build(selector);
        }
    }

    public class LambdaComparer<T> : IEqualityComparer<T>
    {

        private readonly Func<T, T, bool> _equals;
        private readonly Func<T, int> _getHashCode;

        public LambdaComparer(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            _equals = equals.ThrowIfNull();
            _getHashCode = getHashCode.ThrowIfNull();
        }

        public bool Equals(T x, T y)
        {
            return _equals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return _getHashCode(obj);
        }

    }

}
