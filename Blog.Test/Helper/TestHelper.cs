using Blog.Domain.ValueObject;
using Blog.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Blog.Test.Helper;


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
        SeedDatabase(Db);

        AuthorizedClient = factory.CreateClient();
        var token = Task.Run(() => new Authenticator(factory).GetTokenAsync()).GetAwaiter().GetResult();
        AuthorizedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        UnauthorizedClient = factory.CreateClient();
    }

    private void SeedDatabase(BlogDbContext db)
    {
        var username = Username.Create("pooya");
        var password = Password.Create("admin@987");
        var email = Email.Create("admin@gmail.com");
        var user = new Domain.Entity.User(username, password, email, false);

        db.User.Add(user);

        db.Category.Add(new Domain.Entity.Category("Test name", "test description", null));

        db.Article.Add(new Domain.Entity.Article("Test header", "Test title", "Test text", new List<string>(), "link test", DateTime.Now, 1, 1));
        db.Article.Add(new Domain.Entity.Article("Introduction to SQL", "Getting Started with SQL", "An in-depth introduction to SQL.", new List<string> { "SQL", "Database", "Programming" }, "https://example.com/images/sql_intro.jpg", DateTime.Now, 1, 1));
        db.Article.Add(new Domain.Entity.Article("Advanced Python", "Exploring Advanced Python", "Deep dive into advanced Python techniques.", new List<string> { "Python", "Advanced", "Programming" }, "https://example.com/images/python_advanced.jpg", DateTime.Now, 1, 1));
        db.Article.Add(new Domain.Entity.Article("Web Development", "Full Stack with React and Node.js", "Complete guide to full stack development.", new List<string> { "Web Development", "React", "Node.js" }, "https://example.com/images/fullstack.jpg", DateTime.Now, 1, 1));
        db.Article.Add(new Domain.Entity.Article("Machine Learning Basics", "Introduction to Machine Learning", "Guide to the basics of Machine Learning.", new List<string> { "Machine Learning", "AI", "Data Science" }, "https://example.com/images/ml_intro.jpg", DateTime.Now, 1, 1));
        db.Article.Add(new Domain.Entity.Article("Cloud Computing", "Understanding Cloud Computing", "Introduction to Cloud Computing services.", new List<string> { "Cloud Computing", "Technology", "Web Services" }, "https://example.com/images/cloud_computing.jpg", DateTime.Now, 1, 1));
        var articles = db.Article.ToList();
        foreach (var article in articles)
            article.Accept();

        db.Article.UpdateRange(articles);
        
        
        
        db.SaveChanges();

        ClientUserId = user.Id;
    }

    public void Dispose()
    {
        Db.Database.EnsureDeleted();
        Db.Dispose();
    }
}
