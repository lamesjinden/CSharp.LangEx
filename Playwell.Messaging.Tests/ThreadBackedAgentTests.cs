using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Playwell.Messaging.Tests
{

    [TestFixture]
    public class ThreadBackedAgentTests
    {

        [Test]
        public void SynchronousTest()
        {
            using (var agent = new ThreadBackedAgent<string, char>((s, c) => s + c, string.Empty))
            {
                agent.Start();

                agent.Send('a');
                agent.Send('b');
                agent.Send('c');
                agent.Send('d');
                agent.Send('e');

                agent.Stop();

                Assert.That(agent.Value.Length == 5);
            }
        }

        [Test]
        public void AsynchronousTest()
        {
            const int taskCount = 1000;
            var random = new Random(0);
            using (var agent = new ThreadBackedAgent<string, char>((s, c) => { Thread.Sleep(2); return s + c; }, string.Empty))
            {
                agent.Start();

                var tasks =
                    RandomCharacters()
                        .Take(taskCount)
                        .Select(c => Task.Delay(random.Next(0, 251)).ContinueWith(task => agent.Send(c)))
                        .ToArray();

                Task.WaitAll(tasks);

                agent.Stop();

                Assert.That(agent.Value.Length, Is.EqualTo(taskCount));
                Console.WriteLine(agent.Value);
            }
        }

        [Test]
        public void MultiDisposeTest()
        {
            var agent = new ThreadBackedAgent<string, char>((s, c) => s + c, string.Empty);
            agent.Start();
            agent.Dispose();
            agent.Dispose();
        }

        [Test]
        public void DisposeWithoutStart()
        {
            var agent = new ThreadBackedAgent<string, char>((s, c) => s + c, string.Empty);
            agent.Dispose();
        }

        [Test]
        public void SendAfterStop()
        {
            var agent = new ThreadBackedAgent<string, char>((s, c) => s + c, string.Empty);
            agent.Stop();
            Assert.That(() => agent.Send('a'), Throws.InstanceOf<InvalidOperationException>());
        }

        [Test]
        public void SendAfterDispose()
        {
            var agent = new ThreadBackedAgent<string, char>((s, c) => s + c, string.Empty);
            agent.Stop();
            agent.Dispose();
            Assert.That(() => agent.Send('a'), Throws.InstanceOf<ObjectDisposedException>());
        }

        public static IEnumerable<char> RandomCharacters()
        {
            var random = new Random(0);
            while (true)
            {
                yield return (char)random.Next(65, 91);
            }
        }

    }

}
