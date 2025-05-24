using AutoMapper;
using Blog.Application.Model.Article;
using Blog.Application.Service.Article;
using Blog.Domain.Event;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Microsoft.Extensions.Logging;

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
        _mapperMock.Setup(x => x.Map<ArticleViewModel>(article))
            .Returns(new ArticleViewModel(
                article.Id,
                article.Header,
                article.Title,
                article.Text,
                article.Tags,
                article.PublishDate,
                article.Status.ToString(),
                article.Likes,
                article.Dislikes,
                article.Views,
                article.AuthorUserId,
                article.CategoryId,
                article.PreviewImageLink));

        // Act
        _articleService.LikeArticle(10, 1);

        // Assert
        article.Likes.Count.Should().Be(1);
    }

    [Fact]
    public void LikeArticleTwice_ShouldLikesBeInEffective()
    {
        // Arrange
        var article = new Domain.Entity.Article("Test header", "Test title", "", new List<string>(), "", DateTime.Now, 1, 1);
        _articleRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(article);
        _mapperMock.Setup(x => x.Map<ArticleViewModel>(article))
            .Returns(new ArticleViewModel(
                article.Id,
                article.Header,
                article.Title,
                article.Text,
                article.Tags,
                article.PublishDate,
                article.Status.ToString(),
                article.Likes,
                article.Dislikes,
                article.Views,
                article.AuthorUserId,
                article.CategoryId,
                article.PreviewImageLink));

        // Act
        _articleService.LikeArticle(10, 1);
        _articleService.LikeArticle(10, 1);

        // Assert
        article.Likes.Count.Should().Be(0);
    }

    [Fact]
    public void DislikeArticle_ShouldIncreaseDisikes()
    {
        // Arrange
        var article = new Domain.Entity.Article("Test header", "Test title", "", new List<string>(), "", DateTime.Now, 1, 1);
        _articleRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(article);
        _mapperMock.Setup(x => x.Map<ArticleViewModel>(article))
            .Returns(new ArticleViewModel(
                article.Id,
                article.Header,
                article.Title,
                article.Text,
                article.Tags,
                article.PublishDate,
                article.Status.ToString(),
                article.Likes,
                article.Dislikes,
                article.Views,
                article.AuthorUserId,
                article.CategoryId,
                article.PreviewImageLink));

        // Act
        _articleService.DislikeArticle(10, 1);

        // Assert
        article.Dislikes.Count.Should().Be(1);
    }

    [Fact]
    public void DislikeArticleTwice_ShouldLikesBeInEffective()
    {
        // Arrange
        var article = new Domain.Entity.Article("Test header", "Test title", "", new List<string>(), "", DateTime.Now, 1, 1);
        _articleRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(article);
        _mapperMock.Setup(x => x.Map<ArticleViewModel>(article))
            .Returns(new ArticleViewModel(
                article.Id,
                article.Header,
                article.Title,
                article.Text,
                article.Tags,
                article.PublishDate,
                article.Status.ToString(),
                article.Likes,
                article.Dislikes,
                article.Views,
                article.AuthorUserId,
                article.CategoryId,
                article.PreviewImageLink));

        // Act
        _articleService.DislikeArticle(10, 1);
        _articleService.DislikeArticle(10, 1);

        // Assert
        article.Dislikes.Count.Should().Be(0);
    }

    [Fact]
    public void EditArticle_ShouldBeEdited()
    {
        // Arrange
        var article = new Domain.Entity.Article("Test header", "Test title", "", new List<string>(), "", DateTime.Now, 1, 1);
        string header = "new header", title = "new title", text = "new text", imagelink = "new image link";
        var request = new EditArticleRequest(1, header, title, text, imagelink);
        _articleRepositoryMock.Setup(x => x.GetById(1)).Returns(article);


        // Act
        _articleService.EditArticle(request,1);

        // Assert
        article.Should().BeEquivalentTo(new
        {
            Header = header,
            Title = title,
            Text = text,
            PreviewImageLink = imagelink
        });
    }
}
