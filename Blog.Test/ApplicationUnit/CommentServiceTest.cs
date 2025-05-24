using AutoMapper;
using Blog.Application.Model.Comment;
using Blog.Application.Service.Comment;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Microsoft.Extensions.Logging;

namespace Blog.Test.ApplicationUnitTest;

public class CommentServiceTest
{
    private readonly CommentService _commentService;
    private readonly Mock<ICommentRepository> _commentRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<CommentService>> _loggerMock;
    public CommentServiceTest()
    {
        _commentRepositoryMock = new Mock<ICommentRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<CommentService>>();

        _commentService = new CommentService(
            _unitOfWorkMock.Object,
            _commentRepositoryMock.Object,
            _mapperMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public void AddComment_ShouldBeAdded()
    {
        // Arrange
        var text = "comment test";
        var articleId = 1;
        var requst = new AddCommentRequest(text,articleId,null);

        // Act
        var reply = _commentService.AddComment(requst,1);

        // Assert
        Assert.NotNull(reply);
    }
}
