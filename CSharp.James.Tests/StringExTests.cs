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
    public class StringExTests
    {

        [Test]
        public void MaybeTestWithNull()
        {
            string nullString = null;
            Assert.That(nullString.Maybe(), Is.Not.Null);
        }

        [Test]
        public void MaybeTestWithValue()
        {
            string valueString = "value";
            Assert.That(valueString.Maybe(), Is.EqualTo(valueString));
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
