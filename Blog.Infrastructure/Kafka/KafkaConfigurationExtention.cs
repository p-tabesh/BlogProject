using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.Kafka;

public static class KafkaConfigurationExtention
{
    public static void AddBlogKafkaProducer(this IServiceCollection services, IConfiguration configuration)
    {
        var producerConfig = new ProducerConfig()
        {
            BootstrapServers = "localhost:9092",
            ClientId = "post-view-producer"
        };

        var producer = new ProducerBuilder<string,object>(producerConfig).Build();

        services.AddSingleton(producer);
    }
}
