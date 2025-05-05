namespace KafkaConsumer.EventHandler
{
    public interface IEventHandler
    {
        Task HandleAsync(string message);
    }
}
