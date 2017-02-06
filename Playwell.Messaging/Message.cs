using System;

namespace Playwell.Messaging
{
    public class Message
    {
        public Message(Type type, object instance)
        {
            if (type == null) throw new NullReferenceException(nameof(type));
            if (instance == null) throw new NullReferenceException(nameof(instance));
            if (type != instance.GetType()) throw new InvalidOperationException($"{nameof(instance)} must be an instance of {nameof(type)}");
            Type = type;
            Instance = instance;
        }
        public Type Type { get; }
        public object Instance { get; }

        public static Message From(object any)
        {
            return new Message(any.GetType(), any);
        }
    }
}
