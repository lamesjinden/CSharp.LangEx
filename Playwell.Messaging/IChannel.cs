namespace Playwell.Messaging
{
    public interface IChannel<in T>
    {
        void Send(T message);
    }
}