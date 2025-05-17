using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.Extention;

public static class KafkaExtention
{
    public static void AddBlogProducer(this IServiceCollection services, IConfiguration configuration)
    {
        var kafkaSettings = configuration.GetSection("Kafka").Get<KafkaSettings>();
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = kafkaSettings.Producer.BootstrapServer
        };

        services.AddSingleton(new ProducerBuilder<string, string>(producerConfig).Build());
    }

    public static void AddBlogConsumer(this IServiceCollection services, IConfiguration configuration)
    {
        var kafkaSettings = configuration.GetSection("Kafka").Get<KafkaSettings>();
        var consumerConfig = new ConsumerConfig()
        {
            BootstrapServers = kafkaSettings.Consumer.BootstrapServer,
            GroupId = kafkaSettings.Consumer.GroupId,
            ClientId = kafkaSettings.Consumer.ClientId
        };
    }
}

public class KafkaSettings
{
    public KafkaProducerSettings Producer { get; set; }
    public KafkaConsumerSettings Consumer { get; set; }
}

public class KafkaProducerSettings
{
    public string BootstrapServer { get; set; }
}

public class KafkaConsumerSettings
{
    public string BootstrapServer { get; set; }
    public string GroupId { get; set; }
    public string ClientId { get; set; }
}
