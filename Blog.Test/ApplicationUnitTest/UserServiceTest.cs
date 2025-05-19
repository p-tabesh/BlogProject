

using AutoMapper;
using Blog.Application.Model.User;
using Blog.Application.Service.Article;
using Blog.Application.Service.User;
using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Blog.Domain.ValueObject;
using Microsoft.Extensions.Logging;

namespace Blog.Test.ApplicationUnitTest;

public class UserServiceTest
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<UserService>> _loggerMock;
    public UserServiceTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<UserService>>();

        _userService = new UserService(
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _loggerMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public void ChangePassword_PasswordShouldBeChanged()
    {
        // Arrange
        var user = new User(
            Username.Create("username"),
            Password.Create("pooya@admin"),
            Email.Create("email@gmail.com"),
            false);

        var request = new ChangePasswordRequest("pooya@admin", "admin@pooya");
        _userRepositoryMock.Setup(x => x.GetById(1)).Returns(user);

        // Act
        _userService.ChangePassword(request, 1);


        user.Password.Value.Should().Be(Password.Create("pooya@admin").Value);
    }

    [Fact]
    public void ChangePasswordWithWrongOldPassword_ShouldThrowsException()
    {
        // Arrange
        var user = new User(
            Username.Create("username"),
            Password.Create("pooya@admin"),
            Email.Create("email@gmail.com"),
            false);

        var request = new ChangePasswordRequest("xxxx@xxxx", "yyyy@yyyyy");
        _userRepositoryMock.Setup(x => x.GetById(1)).Returns(user);

        // Act
        var ex = Assert.Throws<Exception>(() => _userService.ChangePassword(request, 1));


        // Assert
        ex.Message.Should().Be("old password doesn't match");
    }
}
