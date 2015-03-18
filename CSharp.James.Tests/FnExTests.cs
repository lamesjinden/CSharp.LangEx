using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;
using CSharp.James;

namespace CSharp.James.Tests
{

    [TestFixture]
    public class FnExTests
    {

        [Test]
        public void ReturnActionTest()
        {

            object x = new object();
            Assert.That(
                object.ReferenceEquals(
                    x.Return(() => { }), 
                    x), 
                Is.True);
        }

        [Test]
        public void ReturnFuncTest()
        {
            object x = new object();
            Assert.That(
                object.ReferenceEquals(
                    x.Return((y) => Console.WriteLine(y)), 
                    x), 
                Is.True);
        }

        [Test]
        public void ApplyActionTest()
        {
            const string greeting = "hello";
            bool invoked = false;
            Action<string> flip = 
                x => greeting
                        .Return(() => invoked = true)
                        .Return(y => Console.WriteLine(y));
            
            greeting.Apply(flip);

            Assert.That(invoked, Is.True);
        }

        [Test]
        public void ApplyTest2()
        {
            const string greeting = "hello";
            bool invoked = false;
            Func<string, string> flip =
                x => greeting
                        .Return(() => invoked = true)
                        .Return(y => Console.WriteLine(y));

            greeting.Apply(flip);

            Assert.That(invoked, Is.True);
        }

        [Test]
        public void ChainTest()
        {
            Func<int, double> intToDouble = i => i * 1.0;
            Func<double, string> doubleToString = d => d.ToString();
            Func<string, int> stringToInt = s => int.Parse(s);

            var result =
                10.Apply(intToDouble)
                  .Apply(doubleToString)
                  .Apply(stringToInt);

            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void ComposeTest()
        {
            Func<int, double> intToDouble = i => i * 1.0;
            Func<double, string> doubleToString = d => d.ToString();
            Func<string, int> stringToInt = s => int.Parse(s);

            var roundTrip = intToDouble.Comp(doubleToString).Comp(stringToInt);

            Assert.That(roundTrip(10), Is.EqualTo(10));
        }

    }

}
