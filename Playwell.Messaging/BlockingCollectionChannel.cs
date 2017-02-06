using System;
using System.Collections.Concurrent;

namespace Playwell.Messaging
{
    public class BlockingCollectionChannel<T> : IChannel<T>
    {
        public BlockingCollectionChannel(BlockingCollection<T> blockingCollection)
        {
            if (blockingCollection == null) throw new NullReferenceException(nameof(blockingCollection));
            BlockingCollection = blockingCollection;
        }
        private BlockingCollection<T> BlockingCollection { get; }
        public void Send(T message)
        {
            BlockingCollection.Add(message);
        }
    }
}