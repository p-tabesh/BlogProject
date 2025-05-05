using Confluent.Kafka;
using KafkaConsumer;
using KafkaConsumer.EventHandler;
using StackExchange.Redis;
using System.Text.Json;
using static Confluent.Kafka.ConfigPropertyNames;

class Program
{
    public Program()
    {
        Console.WriteLine("test");
    }
    public static void Main(string[] args)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "blog-event-consumemr",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true
        };
        var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        var topic = "articleView-event";
        var cache = new Dictionary<string, (string,DateTime)>();

        while (true)
        {
            try
            {
                consumer.Subscribe(topic);
                var consumeResult = consumer.Consume();
                //var deserializedData = JsonSerializer.Deserialize<ArticleViewEventModel>(consumeResult.Value);
                //Console.WriteLine($"deserialized data: {deserializedData}");
                cache.Add("dict Key", ("dict value", DateTime.Now));
                Console.WriteLine(cache.Values.First());

            }
            catch (ConsumeException)
            {

            }
        }







    }
}