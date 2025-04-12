using Blog.Infrastructure.Extention;
using Blog.Presentation.Middlewares;
using Blog.Web.Extention;
using Elastic.Serilog.Sinks;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddBlogDbContext(builder.Configuration);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
    {
        AutoRegisterTemplate = true,
        IndexFormat = "blogindex-log"
    }).CreateLogger();


builder.Host.UseSerilog();


builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddUnitOfWork();

builder.Services.AddBlogAuthentication(builder.Configuration);

builder.Services.AddBlogSwaggerConfiguration();

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
