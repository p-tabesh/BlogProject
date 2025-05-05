using Confluent.Kafka;
using KafkaConsumer.EventHandler;
using System.Text.Json;

namespace KafkaConsumer;

public class ConsumerHandler
{
    private IConsumer<string,string> _consumer; 

    public ConsumerHandler(ConsumerConfig consumerConfig,string topic)
    {
        _consumer = new ConsumerBuilder<string,string>(consumerConfig).Build();
    }

    public void Consume(string topic)
    {
        
    }
}
