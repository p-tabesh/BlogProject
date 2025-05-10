using Blog.Application.Model.Article;
using Blog.Infrastructure.Context;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Blog.Test;

[Collection("TestCollection")]
public class ArticleTest
{
    private HttpClient _client;
    private BlogDbContext _db;

    public ArticleTest(TestHelper helper)
    {
        _client = helper.Client;
        _db = helper.Db;
    }

    [Fact]
    public async Task CreateArticle_ReturnsCreated()
    {
        // Arrange
        var requestUrl = "/api/articles";
        var request = new CreateArticleRequest("Test header", "Test title", "Test text", new List<string>(), DateTime.Now, "link test", 1);

        // Act
        var response = await _client.PostAsJsonAsync(requestUrl, request);
        var responseContent = await response.Content.ReadAsStringAsync();
        int.TryParse(responseContent, out int id);
        var article = _db.Article.FirstOrDefault(a => a.Id == id);


        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        article.Should().NotBeNull();
        article.Status.Should().Be(Domain.Enum.Status.Draft);
    }

    [Fact]
    public async Task Get_Articles_ReturnListOfArticle()
    {
        // Arrange 
        var requestUrl = "api/articles";

        // Act
        var response = await _client.GetAsync(requestUrl);
        var responseContent = await response.Content.ReadAsStringAsync();
        var articles = JsonSerializer.Deserialize<List<ArticleViewModel>>(responseContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        // Assert
        response.EnsureSuccessStatusCode();
        responseContent.Should().NotBeEmpty();
        articles.Should().AllBeOfType<ArticleViewModel>();
    }

    [Fact]
    public async Task Get_ArticleById_ReturnAnArticle()
    {
        // Arrange 
        var newArticle = new Domain.Entity.Article("Test header", "Test title", "Test text", new List<string>(), "link test", DateTime.Now, 1, 1);
        _db.Article.Add(newArticle);
        _db.SaveChanges();
        var requestUrl = $"/api/articles/{newArticle.Id}";

        // Act
        var response = await _client.GetAsync(requestUrl);
        var responseContent = await response.Content.ReadAsStringAsync();
        var id = JsonDocument.Parse(responseContent).RootElement.GetProperty("id").GetInt32();
        var json = JsonSerializer.Deserialize<ArticleViewModel>(responseContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        // Assert
        response.EnsureSuccessStatusCode();
        json.Should().BeEquivalentTo(new
        {
            Id = newArticle.Id,
            Header = newArticle.Header,
            Title = newArticle.Title,
            Text = newArticle.Text,
            Tags = newArticle.Tags,
            PublishDate = newArticle.PublishDate,
            status = newArticle.Status,
            Likes = newArticle.Likes,
            Dislikes = newArticle.Dislikes,
            AuthorUserId = newArticle.AuthorUserId,
            CategoryId = newArticle.CategoryId,
            PreviewImageLink = newArticle.PreviewImageLink
        }, options => options.ExcludingMissingMembers());
    }
}