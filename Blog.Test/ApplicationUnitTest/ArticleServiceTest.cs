using AutoMapper;
using Blog.Application.Service.Article;
using Blog.Domain.Event;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Blog.Test.ApplicationUnitTest;

public class ArticleServiceTest
{
    private readonly ArticleService _articleService;
    private readonly Mock<IArticleRepository> _articleRepositoryMock;
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<ArticleService>> _loggerMock;
    private readonly Mock<IEventProducer> _producerMock;

    public ArticleServiceTest()
    {
        _articleRepositoryMock = new Mock<IArticleRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<ArticleService>>();
        _producerMock = new Mock<IEventProducer>();

        _articleService = new ArticleService(
            _unitOfWorkMock.Object,
            _articleRepositoryMock.Object,
            _categoryRepositoryMock.Object,
            _mapperMock.Object,
            _loggerMock.Object,
            _producerMock.Object);
    }

    [Fact]
    public void LikeArticle_ShouldIncreaseLikes()
    {
        // Arrange
        var article = new Domain.Entity.Article("Test header", "Test title", "", new List<string>(), "", DateTime.Now, 1, 1);
        _articleRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(article);

        // Act
        _articleService.LikeArticle(10,1);

        // Assert
        article.Likes.Count.Should().Be(1);
    }

    [Fact]
    public void LikeArticleTwice_ShouldLikesBeInEffective()
    {
        // Arrange
        var article = new Domain.Entity.Article("Test header", "Test title", "", new List<string>(), "", DateTime.Now, 1, 1);
        _articleRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(article);

        // Act
        _articleService.LikeArticle(10, 1);
        _articleService.LikeArticle(10, 1);

        // Assert
        article.Likes.Count.Should().Be(0);
    }

    [Fact]
    public void DislikeArticle_ShouldIncreaseLikes()
    {
        // Arrange
        var article = new Domain.Entity.Article("Test header", "Test title", "", new List<string>(), "", DateTime.Now, 1, 1);
        _articleRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(article);

        // Act
        _articleService.DislikeArticle(10, 1);        

        // Assert
        article.Likes.Count.Should().Be(0);
    }

    [Fact]
    public void DislikeArticleTwice_ShouldLikesBeInEffective()
    {
        // Arrange
        var article = new Domain.Entity.Article("Test header", "Test title", "", new List<string>(), "", DateTime.Now, 1, 1);
        _articleRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(article);

        // Act
        _articleService.DislikeArticle(10, 1);
        _articleService.DislikeArticle(10, 1);

        // Assert
        article.Dislikes.Count.Should().Be(0);
    }
}
