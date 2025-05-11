using Blog.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Blog.Test;

public class BlogWebApplicationFactory<TStartUp> : WebApplicationFactory<TStartUp> where TStartUp : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<BlogDbContext>>();

            services.AddDbContext<BlogDbContext>(option =>
            {
                option.UseInMemoryDatabase("InMemoryDbBlogTest");
            });

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<BlogDbContext>();
                db.Database.EnsureCreated();
            }
        });
    }
}
