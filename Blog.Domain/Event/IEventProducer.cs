namespace Blog.Domain.Event;

public interface IEventProducer
{
    Task ProduceAsync<T>(string topic, T message);
}
