using AutoMapper;
using Blog.Application.Model.User;
using Blog.Application.Service.User;
using Blog.Domain.Entity;
using Blog.Domain.Enum;
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


        user.Password.Should().Be(Password.Create("admin@pooya"));
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

    [Fact]
    public void ChangeUsername_UsernameShouldBeChanged()
    {
        // Arrange
        var user = new User(
            Username.Create("username"),
            Password.Create("pooya@admin"),
            Email.Create("email@gmail.com"),
            false);
        var request = new ChangeUsernameRequest("newUsername");
        _userRepositoryMock.Setup(x => x.GetById(1)).Returns(user);

        // Act
        _userService.ChangeUsername(request, 1);

        // Assert
        Assert.Equal(Username.Create("newUsername"), user.Username);
    }

    [Fact]
    public void ChangeUsername_ShouldThrowsDuplicateUsername()
    {
        // Arrange
        var user = new User(
            Username.Create("username"),
            Password.Create("pooya@admin"),
            Email.Create("email@gmail.com"),
            false);

        var user2 = new User(
            Username.Create("username2"),
            Password.Create("pooya@admin"),
            Email.Create("email@gmail.com"),
            false);

        var request = new ChangeUsernameRequest("username2");
        _userRepositoryMock.Setup(x => x.GetByUsername(Username.Create("username2"))).Returns(user2);

        // Act
        var ex = Assert.Throws<Exception>(() => _userService.ChangeUsername(request, 1));

        // Assert
        Assert.Equal("this username already exists", ex.Message);
    }

    [Fact]
    public void CreateProfile_ShouldBeCreated()
    {
        // Arrange
        var user = new User(
            Username.Create("username"),
            Password.Create("pooya@admin"),
            Email.Create("email@gmail.com"),
            false);
        _userRepositoryMock.Setup(x => x.GetWithProfile(1)).Returns(user);
        var request = new CreateProfileRequest("test name", Domain.Enum.Gender.Male, "Test place", "test bio", "test link");

        // Act
        _userService.CreateProfile(request, 1);

        // Assert
        Assert.NotNull(user.Profile);
    }

    [Fact]
    public void CreateProfile_ProfileAlreadyExists()
    {
        // Arrange
        var user = new User(
            Username.Create("username"),
            Password.Create("pooya@admin"),
            Email.Create("email@gmail.com"),
            false);

        user.CreateProfile("FullName", Domain.Enum.Gender.Male, "BirthPlace", "Bio", "ProfileImageLink");
        var request = new CreateProfileRequest("test name", Domain.Enum.Gender.Male, "Test place", "test bio", "test link");
        _userRepositoryMock.Setup(x => x.GetWithProfile(1)).Returns(user);

        // Act
        var ex = Assert.Throws<Exception>(() => _userService.CreateProfile(request, 1));

        // Assert
        Assert.Equal("User already has Profile.", ex.Message);
    }

    [Fact]
    public void EditProfile_ShouldBeEdited()
    {
        // Arrange
        var user = new User(
            Username.Create("username"),
            Password.Create("pooya@admin"),
            Email.Create("email@gmail.com"),
            false);
        user.CreateProfile("FullName", Domain.Enum.Gender.Male, "BirthPlace", "Bio", "ProfileImageLink");

        _userRepositoryMock.Setup(x => x.GetWithProfile(1)).Returns(user);

        string fullName = "new full name", birthPlace = "new Birthplace", bio = "new bio";
        var gender = Gender.Male;
        var request = new EditProfileRequest(fullName,gender,birthPlace,bio);

        // Act
        _userService.EditProfile(request,1);

        // Assert
        user.Profile.Should().BeEquivalentTo(new
        {
            FullName = fullName,
            BirthPlace = birthPlace,
            Bio = bio,
            Gender = gender
        });
    }
}
