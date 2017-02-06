using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Playwell.Messaging
{
    public class Router
    {
        public Router(BlockingCollection<Message> inbox)
        {
            Inbox = inbox;

            Dispatch = new Dictionary<Type, Action<Message>>
            {
                [typeof(RegisterActor)] = message => DoRegisterActor(((RegisterActor)message.Instance).Actor),
            };

            InboxProcessor = new Thread(ProcessInbox) { IsBackground = true };
            InboxProcessor.Start();
        }

        private BlockingCollection<Message> Inbox { get; }

        private Dictionary<Type, Action<Message>> Dispatch { get; }

        private Thread InboxProcessor { get; }

        private void ProcessInbox()
        {
            foreach (var item in Inbox.GetConsumingEnumerable())
            {
                try
                {
                    var @type = item.Type;
                    var handler = Dispatch[@type];
                    handler(item);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        public void Register(IActor actor)
        {
            if (actor == null) throw new NullReferenceException(nameof(actor));
            Inbox.Add(Message.From(new RegisterActor(actor)));
        }

        private void DoRegisterActor(IActor actor)
        {
            foreach (var subscription in actor.Subscriptions)
            {
                Dispatch.Add(subscription, message => actor.Inbox.Send(message));
            }
        }

        private class RegisterActor
        {
            public RegisterActor(IActor actor)
            {
                Actor = actor;
            }

            public IActor Actor { get; }
        }

    }
}