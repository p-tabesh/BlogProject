using Blog.Infrastructure.Extention;
using Blog.Web.Extention;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddBlogDbContext(builder.Configuration);

builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddUnitOfWork();

builder.Services.AddBlogAuthentication(builder.Configuration);

builder.Services.AddBlogSwaggerConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
