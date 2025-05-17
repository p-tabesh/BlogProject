using Blog.Domain.Entity;
using Blog.Domain.ValueObject;

namespace Blog.Test.DomainUnitTest;

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
}
