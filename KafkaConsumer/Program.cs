

using Confluent.Kafka;
using StackExchange.Redis;

var consumerConfig = new ConsumerConfig()
{
    BootstrapServers = "localhost:9092",
    GroupId = "post-view-counter-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var redis = ConnectionMultiplexer.Connect("localhost");
var db = redis.GetDatabase();

using var consumer = new ConsumerBuilder<string,string>(consumerConfig).Build();
consumer.Subscribe("articleView-event");

while (true)
{
	try
	{
		var consumeResult = consumer.Consume();
		string postId = consumeResult.Message.Key;
        Console.WriteLine(postId);
        Console.WriteLine(consumeResult.Message.Value);
		//db.StringIncrement($"view:{postId}");
		//foreach (var key in redis.GetServer("localhost",6379).Keys(pattern:"views:*"))
		//{
			
		//}
    }
	finally
	{

	}
}