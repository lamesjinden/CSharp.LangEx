using System;
using System.Collections.Generic;

namespace Playwell.Messaging
{
    public interface IActor
    {
        IEnumerable<Type> Subscriptions { get; }

        IChannel<Message> Inbox { get; }
    }
}