using System.Collections.Concurrent;

namespace Playwell.Messaging
{
    public static class MessagingEx
    {
        public static IChannel<T> AsChannel<T>(this BlockingCollection<T> blockingCollection)
        {
            return new BlockingCollectionChannel<T>(blockingCollection);
        }

        public static void Send(this IChannel<Message> channel, object instance)
        {
            channel.Send(new Message(instance.GetType(), instance));
        }
    }
}