using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Playwell.Messaging
{
    public abstract class Actor : IActor
    {
        protected Actor(BlockingCollection<Message> inbox, IChannel<Message> outbox)
        {
            Inbox = inbox;
            Outbox = outbox;

            InboxProcessor = new Thread(ProcessInbox) { IsBackground = true };
            InboxProcessor.Start();
        }

        protected BlockingCollection<Message> Inbox { get; }
        protected IChannel<Message> Outbox { get; }
        private Thread InboxProcessor { get; }

        private void ProcessInbox()
        {
            var handlers = CreateHandlersSafe();
            foreach (var item in Inbox.GetConsumingEnumerable())
            {
                try
                {
                    var @type = item.Type;
                    var handler = handlers[@type];
                    handler(item.Instance);
                }
                catch (Exception exception)
                {
                    OnMessageProcessingException(exception);
                }
            }
        }

        private IDictionary<Type, Action<object>> CreateHandlersSafe()
        {
            try
            {
                return CreateHandlers();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                Console.Error.WriteLine(e);
                throw;
            }
        }
        protected abstract IDictionary<Type, Action<object>> CreateHandlers();

        protected virtual void OnMessageProcessingException(Exception e)
        {
            Console.Error.WriteLine(e);
        }

        public IEnumerable<Type> Subscriptions => CreateHandlers().Keys;

        IChannel<Message> IActor.Inbox => Inbox.AsChannel();

    }
}