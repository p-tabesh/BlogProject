using Blog.Application;
using Blog.Application.Model.Article;
using Blog.Infrastructure.Extention;
using Blog.Web.Middleware;
using Blog.Web.Extention;
using Elastic.Serilog.Sinks;
using Serilog;
using Serilog.Exceptions;
using AutoMapper;
using Blog.Application.Model.Category;
using Confluent.Kafka;


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
Log.Logger = new LoggerConfiguration()
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

builder.Services.AddScoped<AI>();

// Authentication
builder.Services.AddBlogAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

// Swagger Configuration
builder.Services.AddBlogSwaggerConfiguration();

// Kafka Configuration

var producerConfig = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};
builder.Services.AddSingleton(new ProducerBuilder<string, string>(producerConfig).Build());

// Middleware configuration
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();


// Mapper Configuration
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
