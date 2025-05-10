using Blog.Domain.ValueObject;
using Blog.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Net.Http.Headers;

namespace Blog.Test;


public class TestHelper : IDisposable
{

    public BlogDbContext Db { get; init; }
    public HttpClient AuthorizedClient { get; init; }
    public HttpClient UnauthorizedClient { get; init; }
    public int ClientUserId { get; private set; }

    public TestHelper()
    {
        var factory = new BlogWebApplicationFactory<Program>();
        var scope = factory.Services.CreateScope();
        Db = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
        SeedData(Db);

        AuthorizedClient = factory.CreateClient();
        var token = Task.Run(() => new Authenticator(factory).GetTokenAsync()).GetAwaiter().GetResult();
        AuthorizedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        UnauthorizedClient = factory.CreateClient();
    }

    private void SeedData(BlogDbContext db)
    {
        var username = Username.Create("pooya");
        var password = Password.Create("admin@987");
        var email = Email.Create("admin@gmail.com");
        var user = new Domain.Entity.User(username, password, email, false);

        db.User.Add(user);
        db.Category.Add(new Domain.Entity.Category("Test name", "test description", null));
        db.Article.Add(new Domain.Entity.Article("Test header", "Test title", "Test text", new List<string>(), "link test", DateTime.Now, 1, 1));
        
        db.SaveChanges();

        ClientUserId = user.Id;
    }

    public void Dispose()
    {
        Db.Database.EnsureDeleted();
        Db.Dispose();
    }
}
