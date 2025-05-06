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
        Console.Write("consumer service");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _consumer.Subscribe(_topic);
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = await Task.Run(() => _consumer.Consume(stoppingToken), stoppingToken);
                Console.WriteLine("Consumer Key: " + consumeResult.Key);
                if (consumeResult != null)
                {
                    var message = JsonSerializer.Deserialize<ArticleViewEventModel>(consumeResult.Value);
                    await _redis.StringSetAsync($"view:{message.ConnectionId}", message.ArticleId.ToString());
                }
                Console.WriteLine("loop");
                await Task.Delay(1000, stoppingToken);
            }

        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            _consumer.Close();
        }

    }
}
