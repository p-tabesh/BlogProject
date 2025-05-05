using System.Text.Json;

namespace KafkaConsumer.EventHandler;

public class PostViewEventHandler : IEventHandler
{
    public Task HandleAsync(string message)
    {

        JsonSerializer.Deserialize<ArticleViewEventModel>(message);
        return Task.CompletedTask;
    }
}


public record ArticleViewEventModel(int Id, string ConnectionId);