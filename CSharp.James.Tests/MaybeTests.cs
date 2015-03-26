using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSharp.James.Tests
{

    [TestFixture]
    public class MaybeTests
    {

        [Test]
        public void MaybeWithNull()
        {
            var maybe = Maybe<string>.None;
            Assert.That(maybe.HasValue, Is.False);
        }

        [Test]
        public void EmptyMaybeThrowsOnGetValue()
        {
            var maybe = Maybe<string>.None;
            Assert.That(() => maybe.Value, Throws.Exception);
        }

        [Test]
        public void MaybeWithValue()
        {
            var maybe = new Maybe<string>(".");
            Assert.That(maybe.HasValue, Is.True);
        }

        [Test]
        public void MaybeEqual()
        {
            var m1 = new Maybe<string>(".");
            var m2 = new Maybe<string>(".");
            Assert.That(m1 == m2, Is.True);
        }

        [Test]
        public void MaybeNotEqual()
        {
            var m1 = new Maybe<string>(".");
            var m2 = new Maybe<string>("..");
            Assert.That(m1 == m2, Is.False);
            Assert.That(m1 != m2, Is.True);
        }

        [Test]
        public void GetHashCodeThrows()
        {
            //because this still doesn't make sense to me
            var maybe = new Maybe<string>(".");
            Assert.That(() => maybe.GetHashCode(), Throws.Exception);
        }

        [Test]
        public void EmptyMaybeSelect()
        {
            var maybe = Maybe<string>.None;
            Assert.That(maybe.Select(s => s.ToCharArray()).HasValue, Is.False);
        }

        [Test]
        public void MaybeWithValueSelect()
        {
            var maybe = new Maybe<string>(".");
            Assert.That(maybe.Select(s => s.ToCharArray()).HasValue, Is.True);
        }

        [Test]
        public void EmptyMaybeWhere()
        {
            var maybe = Maybe<string>.None;
            Assert.That(maybe.Where(s => s.Length < 3).HasValue, Is.False);
        }

        [Test]
        public void MaybeWithValueWherePass()
        {
            var maybe = new Maybe<string>("123");
            Assert.That(maybe.Where(s => s.Length == 3).HasValue, Is.True);
        }

        [Test]
        public void MaybeWithValueWhereFail()
        {
            var maybe = new Maybe<string>("123");
            Assert.That(maybe.Where(s => s.Length == 5).HasValue, Is.False);
        }

        class Person
        {
            public Person(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }

            public Person(string firstName, string middleName, string lastName)
                : this(firstName, lastName)
            {
                MiddleName = middleName;
            }

            public string FirstName { get; set; }
            public string LastName { get; set;}
            public Maybe<string> MiddleName { get; set; }
        }

        [Test]
        public void EmptyMaybeSelectManyTest()
        {
            var maybePerson = Maybe<Person>.None;
            var maybeMiddle = maybePerson.SelectMany(p => p.MiddleName, (p, m) => m);
            Assert.That(maybeMiddle.HasValue, Is.False);
        }

        [Test]
        public void SelectManyTest()
        {
            var maybePerson = new Maybe<Person>(new Person("first", "middle", "last"));
            var maybeMiddle = maybePerson.SelectMany(p => p.MiddleName);
            Assert.That(maybeMiddle.HasValue, Is.True);
            Assert.That(maybeMiddle.Value, Is.EqualTo("middle"));
        }

        [Test]
        public void SelectManyTestWithResultProjection()
        {
            var maybePerson = new Maybe<Person>(new Person("first", "middle", "last"));
            var maybeMiddle = maybePerson.SelectMany(p => p.MiddleName, (p, m) => m.ToList());
            Assert.That(maybeMiddle.HasValue, Is.True);
        }

        [Test]
        public void SelectManyTestIntermediateFail()
        {
            var maybePerson = new Maybe<Person>(new Person("first", "last"));
            var maybeMiddle = maybePerson.SelectMany(p => p.MiddleName, (p, m) => m);
            Assert.That(maybeMiddle.HasValue, Is.False);
        }

    }

}
