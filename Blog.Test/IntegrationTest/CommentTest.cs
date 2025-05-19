using Blog.Application.Model.Comment;
using Blog.Domain.ValueObject;
using Blog.Infrastructure.Context;
using Blog.Test.Helper;
using Elastic.Clients.Elasticsearch.Core.TermVectors;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Blog.Test.IntegrationTest;

public class CommentTest : IClassFixture<BlogWebApplicationFactory<Program>>//,IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly BlogDbContext _db;

    public CommentTest(BlogWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
        var scope = _factory.Services.CreateScope();
        _db = scope.ServiceProvider.GetRequiredService<BlogDbContext>();

        SeedData(_db);
        var token = Task.Run(()=> new Authenticator(factory).GetTokenAsync()).GetAwaiter().GetResult();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
    }

    [Fact]
    public async Task AddComment_ShouldBeCreated()
    {
        // Arrange
        var articleId = _db.Article.First().Id; 
        var requestUrl = "/api/comments";
        var request = new AddCommentRequest("comment test",articleId,null);

        // Act
        var reply = await _client.PostAsJsonAsync(requestUrl, request);
        var createdCommentId = await reply.Content.ReadAsStringAsync();
        var comment = _db.Comment.First(c => c.Id == Convert.ToInt32(createdCommentId));
        // Assert
        reply.Should().NotBeNull();
        comment.Should().NotBeNull();
        
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
    }

    //void IDisposable.Dispose()
    //{
    //    _factory.Dispose();
    //    _db.Dispose();
    //    _client.Dispose();
    //}
}
