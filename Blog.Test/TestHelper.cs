using Blog.Domain.ValueObject;
using Blog.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Blog.Test;


public class TestHelper : IDisposable//: IClassFixture<BlogWebApplicationFactory<Program>>
{

    public BlogDbContext Db { get; init; }
    public HttpClient Client { get; init; }

    public TestHelper()
    {
        var factory = new BlogWebApplicationFactory<Program>();
        var scope = factory.Services.CreateScope();
        Db = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
        SeedData(Db);

        Client = factory.CreateClient();
        var token = Task.Run(() => new Authenticator(factory).GetTokenAsync()).GetAwaiter().GetResult();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

    public void Dispose()
    {
        Db.Database.EnsureDeleted();
        Db.Dispose();
    }
}
