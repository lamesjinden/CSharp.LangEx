using System;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using Is = NUnit.Framework.Is;

namespace PlayWell.Core.Tests
{

    [TestFixture]
    public class EitherTests
    {

        [Test]
        public void TestWithFailure()
        {
            Func<string, int> parse = (str) => int.Parse(str);
            Func<string, Either<int>> safer = parse.Tries();
            
            var either = safer("XI");
            Assert.That(either.HasError, Is.True);
            Assert.That(either.HasValue, Is.False);
        }

        [Test]
        public void TestWithWin()
        {
            Func<string, int> parse = (str) => int.Parse(str);
            Func<string, Either<int>> safer = parse.Tries();

            var either = safer("0");

            Assert.That(either.HasError, Is.False);
            Assert.That(either.HasValue, Is.True);
        }

    }

}
