namespace Blog.Domain.Event;

public interface IEventConsumer
{
    Task ConsumeAsync<T>(string topic, T message);
}
