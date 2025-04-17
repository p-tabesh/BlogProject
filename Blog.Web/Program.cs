using Blog.Application.Model.Article;
using Blog.Infrastructure.Extention;
using Blog.Presentation.Middlewares;
using Blog.Web.Extention;
using Elastic.Serilog.Sinks;
using Serilog;
using Serilog.Exceptions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddBlogDbContext(builder.Configuration);

// Config Logger
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
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

// Authentication
builder.Services.AddBlogAuthentication(builder.Configuration);

// Swagger Configuration
builder.Services.AddBlogSwaggerConfiguration();

// Middleware configuration
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();


// Mapper Configuration
builder.Services.AddAutoMapper(typeof(ArticleProfile).Assembly);


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
