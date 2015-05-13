using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PlayWell.Core.Tests
{

    [TestFixture]
    public class DictionaryExTests
    {

        [Test]
        public void AddAndTest()
        {
            var dictionary = new Dictionary<int, int>();
            var result = dictionary.AddAnd(1, 1).AddAnd(2, 2).AddAnd(3, 3);
            Assert.That(result[1], Is.EqualTo(1));
            Assert.That(result[2], Is.EqualTo(2));
            Assert.That(result[3], Is.EqualTo(3));
        }

        [Test]
        public void AddAndTestKeyExists()
        {
            var dictionary = new Dictionary<int, int>();
            Assert.That(() => dictionary.AddAnd(1, 1).AddAnd(1, 1), Throws.Exception);
        }

        [Test]
        public void UpsertAndTest()
        {
            var dictionary = new Dictionary<int, int>();
            var result = dictionary.AddAnd(1, 1).UpsertAnd(1, 2);
            Assert.That(result[1], Is.EqualTo(2));
        }

        [Test]
        public void RemoveAndTest()
        {
            var dictionary = new Dictionary<int, int>();
            dictionary
                .AddAnd(1, 1)
                .AddAnd(2, 2)
                .AddAnd(3, 3)
                .RemoveAnd(1)
                .RemoveAnd(2);
            Assert.That(dictionary.Count, Is.EqualTo(1));
            Assert.That(dictionary[3], Is.EqualTo(3));
        }

        [Test]
        public void GetEmptyTest()
        {
            var dictionary = new Dictionary<string, string>();
            var value = dictionary.Get("A");
            Assert.That(value.HasValue, Is.False);
        }

        [Test]
        public void GetTest()
        {
            var dictionary = new Dictionary<string, string> { {"A", "A" } };
            var capitalA = dictionary.Get("A");
            Assert.That(capitalA.HasValue, Is.True);
            Assert.That(capitalA.Value, Is.EqualTo("A"));
        }

        [Test]
        public void GetOrDefaultTest()
        {
            var dictionary = new Dictionary<string, string>();
            var maybeCount = dictionary.GetOrDefault("count");

            Assert.That(maybeCount, Is.Null);
        }

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
