using Microsoft.VisualStudio.TestTools.UnitTesting;
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

    [TestClass]
    [TestFixture]
    public class FnExTests
    {

        [Test]
        [TestMethod]
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
        [TestMethod]
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
        [TestMethod]
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
        [TestMethod]
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

    }

}
