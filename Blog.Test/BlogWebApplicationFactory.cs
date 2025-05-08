using Blog.Domain.ValueObject;
using Blog.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Test;

public class BlogWebApplicationFactory<TStartUp> : WebApplicationFactory<TStartUp> where TStartUp : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(DbContextOptions<BlogDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

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

                SeedData(db);
            }
        });
    }



    private void SeedData(BlogDbContext db)
    {
        var username = Username.Create("pooya");
        var password = Password.Create("admin@987");
        var email = Email.Create("admin@gmail.com");

        db.User.Add(new Domain.Entity.User(username, password, email, false));
        db.Category.Add(new Domain.Entity.Category("Test name", "test description", null));
        db.SaveChanges();
    }
}
