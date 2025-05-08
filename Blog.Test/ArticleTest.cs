using Blog.Application.Model.Article;
using Blog.Infrastructure.Context;
using Elastic.Transport;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace Blog.Test;

public class ArticleTest : IClassFixture<BlogWebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly string _token;
    private readonly WebApplicationFactory<Program> _factory;

    public ArticleTest(BlogWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
        //_token = Task.Run(() => new Authenticator(factory).GetTokenAsync()).GetAwaiter().GetResult();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
    }

    [Fact]
    public async Task CreateArticle_ReturnsCreated()
    {
        // Creation
        int id;
        {
            // Arrange
            var requestUrl = "/api/articles";
            var request = new CreateArticleRequest("Test header", "Test title", "Test text", new List<string>(),DateTime.Now,"link test",1);
            
            // Act
            var response = await _httpClient.PostAsJsonAsync(requestUrl, request);
            var responseContent = await response.Content.ReadAsStringAsync();
            int.TryParse(responseContent, out id);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        // Check if exists in db
        //{
        //    using var scope = _factory.Services.CreateScope();
        //    var db = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
        //    var article = db.Article.FirstOrDefault(a => a.Id == id);
        //    article.Should().NotBeNull();
        //    article.Status.Should().Be(Domain.Enum.Status.Draft);
        //}
    }


    [Fact]
    public async Task Get_Articles_ReturnListOfArticle()
    {
        // Arrange 
        var requestUrl = "api/articles";

        // Act
        var response = await _httpClient.GetAsync(requestUrl);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().NotBeEmpty();

        // Assert
        using var scope = _factory.Services.CreateScope();
        scope.ServiceProvider.GetRequiredService<BlogDbContext>();
    }

    [Fact]
    public async Task Get_ArticleById_ReturnAnArticle()
    {
    //    using var scope = _factory.Services.CreateScope();
    //    var db = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
    //    var article = db.Article.FirstOrDefault();

        //var requestUrl = $"/api/articles/{article.Id}";
        //var response = await _httpClient.GetAsync(requestUrl);
        //response.EnsureSuccessStatusCode();
    }
}