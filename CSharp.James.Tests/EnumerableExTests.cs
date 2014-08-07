using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace CSharp.James.Tests
{

    [TestFixture]
    [TestClass]
    public class EnumerableExTests
    {

        [TestMethod]
        [Test]
        public void MaybeTestWithNull()
        {
            IEnumerable<int> ints = null;
            Assert.That(ints.Maybe(), Is.Not.Null);
        }

        [TestMethod]
        [Test]
        public void MaybeTestWithValue()
        {
            IEnumerable<int> ints = new[] { 0, 1, 2 };
            Assert.That(ints.Maybe(), Is.EquivalentTo(ints));
        }

        [TestMethod]
        [Test]
        public void TapTest()
        {
            IEnumerable<int> ints = new[] { 0, 1, 2 };
            int count = 0;
            var tapped = ints.Tap((_) => count++).ToList();

            Assert.That(count, Is.EqualTo(3));
            Assert.That(tapped, Is.EquivalentTo(ints));
        }
        
        [TestMethod]
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

        [TestMethod]
        [Test]
        public void WhereAnyTest()
        {
            var positives = new[] { 1, 2, 3 };
            Func<int, bool> isEven = x => x % 2 == 0;
            Func<int, bool> isOdd = x => x % 2 == 1;

            var filtered = positives.WhereAny(isEven, isOdd);

            Assert.That(filtered, Is.EquivalentTo(positives));
        }

        [TestMethod]
        [Test]
        public void GetOrDefaultTest()
        {
            var dictionary = new Dictionary<string, string>();
            
            var maybeCount =
                dictionary
                    .GetOrDefault("count")
                    .IfNotNull(new Func<string, int>(int.Parse).Comp(StructEx.Nullify));

            Assert.That(maybeCount.HasValue, Is.EqualTo(false));
        }

        [TestMethod]
        [Test]
        public void GetOrDefaultTest2()
        {
            var dictionary = new Dictionary<string, string>();
            
            string count = dictionary.GetOrDefault("count");
            var maybeCount =
                count != null
                    ? (int?)int.Parse(count)
                    : null;

            Assert.That(maybeCount.HasValue, Is.EqualTo(false));
        }

    }

}
