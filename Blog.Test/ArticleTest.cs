using Blog.Application.Model.Article;
using Blog.Infrastructure.Context;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Blog.Test;

[Collection("TestCollection")]
public class ArticleTest
{
    private HttpClient _client;
    private HttpClient _unauthorizedClient;
    private BlogDbContext _db;
    private TestHelper _helper;

    public ArticleTest(TestHelper helper)
    {
        _client = helper.AuthorizedClient;
        _db = helper.Db;
        _helper = helper;
        _unauthorizedClient = helper.UnauthorizedClient;
    }

    [Fact]
    public async Task CreateArticle_ReturnsCreated_And_ShouldBeDraft()
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
    public async Task CreateArticle_ReturnsUnauthorize()
    {
        // Arrange
        var requestUrl = "/api/articles";
        var request = new CreateArticleRequest("Test header", "Test title", "Test text", new List<string>(), DateTime.Now, "link test", 1);

        // Act
        var response = await _unauthorizedClient.PostAsJsonAsync(requestUrl, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
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
        newArticle.Accept();
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


    [Fact]
    public async Task LikeArticle_IncreaseArticleLikes()
    {
        // Arrange
        var article = _db.Article.FirstOrDefault();
        var requestUrl = $"/api/articles/{article.Id}/like";

        // Act
        var response = await _client.PostAsync(requestUrl, null);

        // Assert
        response.EnsureSuccessStatusCode();
        var likedArticle = await _db.Article.AsNoTracking().FirstOrDefaultAsync(a => a.Id == article.Id);
        likedArticle.Likes.Count().Should().BeGreaterThan(0);
        var likedBy = likedArticle.Likes.First(x => x == _helper.ClientUserId).Should().NotBe(null);
    }



    [Fact]
    public async Task LikeArticle_ReturnsUnathorized()
    {
        // Arrange
        var requestUrl = $"/api/articles/0/like";

        // Act
        var response = await _unauthorizedClient.PostAsync(requestUrl, null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task DislikeArticle_IncreaseArticleDislikes()
    {
        // Arrange
        var article = _db.Article.FirstOrDefault();
        var requestUrl = $"/api/articles/{article.Id}/dislike";

        // Act
        var response = await _client.PostAsync(requestUrl, null);

        // Assert
        response.EnsureSuccessStatusCode();
        var likedArticle = await _db.Article.AsNoTracking().FirstOrDefaultAsync(a => a.Id == article.Id);
        likedArticle.Dislikes.Count().Should().BeGreaterThan(0);
        var likedBy = likedArticle.Dislikes.First(x => x == _helper.ClientUserId).Should().NotBe(null);
    }



    [Fact]
    public async Task DislikeArticle_ReturnsUnathorized()
    {
        // Arrange
        var requestUrl = $"/api/articles/0/dislike";

        // Act
        var response = await _unauthorizedClient.PostAsync(requestUrl, null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    public async Task GetArticlesWithFilter_ReturnListOfArticle()
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

    public async Task GetArticlesWithSearch_ReturnListOfArticle()
    {
        // Arrange 
        var requestUrl = "api/articles";
        var searchKey = "test";

        // Act
        var response = await _client.GetAsync(requestUrl);
        var responseContent = await response.Content.ReadAsStringAsync();
        var articles = JsonSerializer.Deserialize<List<ArticleViewModel>>(responseContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        // Assert
        response.EnsureSuccessStatusCode();
        responseContent.Should().NotBeEmpty();
        articles.Should().AllBeOfType<ArticleViewModel>();
    }

}