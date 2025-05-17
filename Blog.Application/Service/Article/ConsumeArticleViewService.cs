using Blog.Application.Model.Article;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System.Text.Json;

namespace Blog.Application.Service.Article;

public class ConsumeArticleViewService : BackgroundService
{
    private readonly IConsumer<string, string> _consumer;
    private readonly string _topic;
    private readonly StackExchange.Redis.IDatabase _redis;

    public ConsumeArticleViewService(IConsumer<string, string> consumer, ConnectionMultiplexer connectionMultiplexer, string topic)
    {
        _redis = connectionMultiplexer.GetDatabase();
        _consumer = consumer;
        _topic = topic;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _consumer.Subscribe(_topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.Write("salam");
                var consumeResult = await Task.Run(() => _consumer.Consume(stoppingToken));

                if (consumeResult != null)
                {
                    var message = JsonSerializer.Deserialize<ArticleViewEventModel>(consumeResult.Value);
                    await _redis.StringSetAsync($"view:{message.ConnectionId}", message.ArticleId.ToString());
                }
            }
        }
        catch (Exception)
        {
            // Ignore
        }
        finally
        {
            _consumer.Close();
        }
    }
}
