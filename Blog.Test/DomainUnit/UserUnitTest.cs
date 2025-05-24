using Blog.Domain.Entity;
using Blog.Domain.Enum;
using Blog.Domain.ValueObject;

namespace Blog.Test.Unit;

public class UserUnitTest
{
    [Fact]
    public void CreateBadUsername_ShouldThrowsArgumantException_And_CheckExceptionMessage()
    {
        // Arrange
        var userName = "tes";

        // Act (And Assert)
        var exception = Assert.Throws<ArgumentException>(() => Username.Create(userName));

        // Assert
        Assert.Equal(exception.Message, "Username invalid");
    }

    [Fact]
    public void CreateBadPassword_ShouldThrowsArgumantException_And_CheckExceptionMessage()
    {
        // Arrange
        var password = "test";

        // Act (And Assert)
        var exception = Assert.Throws<ArgumentException>(() => Password.Create(password));

        // Assert
        Assert.Equal(exception.Message, "Password insecure");
    }

    [Fact]
    public void CheckUsernameEquality_ShouldRetursTrue()
    {
        // Arrange
        var username1 = Username.Create("UserName");
        var username2 = Username.Create("UserName");

        // Act
        var equalOperator = username2 == username1;
        var equalsMethod = username1.Equals(username2);

        // Assert
        Assert.True(equalOperator);
        Assert.True(equalsMethod);
    }

    [Fact]
    public void CheckUsernameEquality_ShouldReturnFalse()
    {
        // Arrange
        var username1 = Username.Create("UserName");
        var username2 = Username.Create("UserName");

        // Act
        var notEqualOperator = username2 != username1;


        // Assert
        Assert.False(notEqualOperator);
    }

    [Fact]
    public void CheckUsernameNotEqualt_ShouldReturnTrue()
    {
        // Arrange
        var username1 = Username.Create("UserName1");
        var username2 = Username.Create("UserName2");

        // Act
        var equalOperator = username1 == username2;
        var equalMethod = username1.Equals(username2);

        // Assert
        Assert.False(equalOperator);
        Assert.False(equalMethod);
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

        // Act
        user.ChangePassword(Password.Create("pooya@admin"), Password.Create("admin@pooya"));

        // Assert
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

        // Act
        var ex = Assert.Throws<Exception>(() => user.ChangePassword(Password.Create("xxxx@xxxx"), Password.Create("yyyy@yyyyy")));


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
        var newUsername = Username.Create("newUsername");

        // Act
        user.ChangeUsername(newUsername);

        // Assert
        Assert.Equal(newUsername, user.Username);
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

        // Act
        user.CreateProfile("fn", Gender.Male,"bp","bio","pfplink");

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

        // Act
        var ex = Assert.Throws<InvalidOperationException>(() => user.CreateProfile("fulname", Gender.Male, "bp", "bio", "pfplink"));

        // Assert
        Assert.Equal("user already has profile", ex.Message);
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

        string fullName = "new full name", birthPlace = "new Birthplace", bio = "new bio";
        var gender = Gender.Male;

        // Act
        user.EditProfile(fullName,gender,birthPlace,bio);

        // Assert
        user.Profile.Should().BeEquivalentTo(new
        {
            FullName = fullName,
            BirthPlace = birthPlace,
            Bio = bio,
            Gender = gender
        });
    }

    [Fact]
    public void CreateProfileWithEmptyValues_ThrowsNullException()
    {
        // Arrange
        var user = new User(
            Username.Create("username"),
            Password.Create("pooya@admin"),
            Email.Create("email@gmail.com"),
            false);

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => user.CreateProfile("", Gender.Male, "", "", ""));        
    }
}
