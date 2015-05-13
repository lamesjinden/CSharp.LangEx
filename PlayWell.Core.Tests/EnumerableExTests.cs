using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace PlayWell.Core.Tests
{

    [TestFixture]
    public class EnumerableExTests
    {
       
        [Test]
        public void MaybeTestWithNull()
        {
            IEnumerable<int> ints = null;
            Assert.That(ints.OrEmpty(), Is.Not.Null);
        }

        [Test]
        public void MaybeTestWithValue()
        {
            IEnumerable<int> ints = new[] { 0, 1, 2 };
            Assert.That(ints.OrEmpty(), Is.EquivalentTo(ints));
        }

        [Test]
        public void TapTest()
        {
            IEnumerable<int> ints = new[] { 0, 1, 2 };
            int count = 0;
            var tapped = ints.Tap((_) => count++).ToList();

            Assert.That(count, Is.EqualTo(3));
            Assert.That(tapped, Is.EquivalentTo(ints));
        }
        
        [Test]
        public void WhereAllTest()
        {
            var positives = new[] { 1, 2, 3 };
            Func<int,bool> gtZero = x => x > 0;
            Func<int,bool> ltFour = x => x < 4;
            Func<int,bool> equalsTwo = x => x == 2;
            
            var filtered = positives.WhereAll(gtZero, ltFour, equalsTwo);
            
            Assert.That(filtered.Count(), Is.EqualTo(1));
            Assert.That(filtered.First(), Is.EqualTo(2));
        }

        [Test]
        public void WhereAnyTest()
        {
            var positives = new[] { 1, 2, 3 };
            Func<int, bool> isEven = x => x % 2 == 0;
            Func<int, bool> isOdd = x => x % 2 == 1;

            var filtered = positives.WhereAny(isEven, isOdd);

            Assert.That(filtered, Is.EquivalentTo(positives));
        }

    }

}
