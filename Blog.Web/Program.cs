using Blog.Application.Model.Article;
using Blog.Web.Middleware;
using Blog.Web.Extention;
using Elastic.Serilog.Sinks;
using Serilog;
using Serilog.Exceptions;
using AutoMapper;
using Blog.Application.Model.Category;
using Confluent.Kafka;
using StackExchange.Redis;
using Microsoft.EntityFrameworkCore;
using Blog.Infrastructure.Extention;
using Blog.Application.Service.Article;
using Blog.Domain.Event;
using Blog.Infrastructure.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(
    option =>
    {
        option.AddPolicy("AllowAll", builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
    });

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddBlogDbContext(builder.Configuration);

// Config Logger
Serilog.Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
    {
        AutoRegisterTemplate = true,
        IndexFormat = "blogindex-log"
    }).CreateLogger();

builder.Host.UseSerilog();

// Config Services
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddUnitOfWork();

//builder.Services.AddScoped<AI>();


// Authentication
builder.Services.AddBlogAuthentication(builder.Configuration);
builder.Services.AddAuthorization();


// Swagger Configuration
builder.Services.AddBlogSwaggerConfiguration();


// Kafka Configuration
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection("Kafka:Producer"));
builder.Services.AddScoped<IEventProducer, KafkaEventProducer>();

var consumerConfig = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "ArticleView-ConsumerGroup",
    ClientId = "ArticleView-EventConsumer"
};

builder.Services.AddSingleton(new ConsumerBuilder<string, string>(consumerConfig).Build());

// Middleware configuration
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

// Config Redis
builder.Services.AddSingleton(sp =>
{
    return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnectionString"));
});


// Add hosted services
builder.Services.AddHostedService<ConsumeArticleViewService>(sp =>
{
    var consumer = sp.GetRequiredService<IConsumer<string, string>>();
    var connectionMultiplexer = sp.GetRequiredService<ConnectionMultiplexer>();
    var topic = "articleView-event";
    return new ConsumeArticleViewService(consumer, connectionMultiplexer, topic);
});

builder.Services.AddHostedService<ProcessArticleViewService>();

//// Mapper Configuration
var mapperConfiguration = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<ArticleProfileMapper>();
    cfg.AddProfile<CategoryProfileMapper>();
});

IMapper mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(50);
});

builder.Services.AddBlogRedis(builder.Configuration);


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllers();

app.Run();


public partial class Program { }