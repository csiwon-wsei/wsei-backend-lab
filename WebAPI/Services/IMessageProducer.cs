namespace Web.Services;

public interface IMessageProducer
{
    void SendMessage<T> (T message);
}