using Blog.Domain.ValueObject;
using Blog.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Blog.Test;

public class BlogWebApplicationFactory<TStartUp> : WebApplicationFactory<TStartUp> where TStartUp : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {

            services.AddInMemoryBlogDbContext();
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<BlogDbContext>();
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
            SeedData(db);
        });
    }

    private void SeedData(BlogDbContext db)
    {
        var username = Username.Create("pooya");
        var password = Password.Create("admin@987");
        var email = Email.Create("admin@gmail.com");

        db.User.Add(new Domain.Entity.User(username, password, email, false));
        db.SaveChanges();
    }


}
