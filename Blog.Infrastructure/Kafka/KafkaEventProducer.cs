using Blog.Domain.Event;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Blog.Infrastructure.Kafka;

public class KafkaEventProducer : IEventProducer
{
    private readonly ProducerConfig _producerConfig;
    public KafkaEventProducer(IOptions<ProducerConfig> configOptions)
    {
        _producerConfig = configOptions.Value;
    }

    public async Task ProduceAsync<T>(string topic, T message)
    {
        using var producer = new ProducerBuilder<string, string>(_producerConfig)
            .SetKeySerializer(Serializers.Utf8)
            .SetValueSerializer(Serializers.Utf8)
            .Build();

        var eventMessage = new Message<string, string>()
        {
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(message, message.GetType())
        };

        await producer.ProduceAsync(topic, eventMessage);
    }
}
