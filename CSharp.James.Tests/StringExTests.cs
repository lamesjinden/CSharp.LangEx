using System;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace PlayWell.Core.Tests
{

    [TestFixture]
    public class StringExTests
    {

        [Test]
        public void MaybeTestWithNull()
        {
            string nullString = null;
            Assert.That(nullString.OrEmpty(), Is.Not.Null);
        }

        [Test]
        public void MaybeTestWithValue()
        {
            string valueString = "value";
            Assert.That(valueString.OrEmpty(), Is.EqualTo(valueString));
        }

        [Test]
        public void ThrowIfNullOrWhiteSpaceTestWithNull()
        {
            string nullString = null;
            Assert.That(() => nullString.ThrowIfNullOrWhiteSpace(), Throws.InstanceOf<ArgumentException>());
        }

        [Test]
        public void ThrowIfNullOrWhiteSpaceTestWithEmpty()
        {
            string emptyString = string.Empty;
            Assert.That(() => emptyString.ThrowIfNullOrWhiteSpace("message", "paramName"), Throws.InstanceOf<ArgumentException>());
        }
        
        
        [Test]
        public void ThrowIfNullOrWhiteSpaceTestWithValue()
        {
            string valueString = "value";
            Assert.That(() => valueString.ThrowIfNullOrWhiteSpace(), Throws.Nothing);
            Assert.That(() => valueString.ThrowIfNullOrWhiteSpace(), Is.Not.Null);
        }

    }

}
