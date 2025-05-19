using Blog.Domain.Entity;
using FluentAssertions;

namespace Blog.Test.DomainUnitTest;

public class CommentUnitTest
{

    [Fact]
    public void ShowComment_ShouldShowPropertyBeTrue()
    {
        // Arrange
        var text = "comment test";
        var comment = new Comment(text, null, 1, 1);

        // Act
        comment.Show();

        // Assert
        Assert.Equal(true, comment.IsShow);
    }

    [Fact]
    public void HideAcceptedComment_ShouldShowPropertyBeFalse()
    {
        // Arrange
        var text = "comment test";
        var comment = new Comment(text, null, 1, 1);
        comment.Show();

        // Act
        comment.Hide();

        // Assert
        Assert.Equal(false, comment.IsShow);
    }

    [Fact]
    public void ReplyComment_ParentAndChildrenShouldNotBeNull()
    {
        // Arrange
        var text = "comment test";
        var comment = new Comment(text, null, 1, 1);
        comment.Show();

        // Act
        comment.ReplyComment("reply test", 1);

        // Assert
        comment.ChildrenComments.Should().NotBeNull();
        comment.ChildrenComments.First().RelatedCommentId.Should().NotBeNull();
    }

    [Fact]
    public void LikeComment_LikesShouldIncreased()
    {
        // Arrange
        var text = "comment test";
        var comment = new Comment(text, null, 1, 1);
        var likerId = 5;

        // Act
        comment.Like(likerId);

        // Assert
        Assert.Equal(1, comment.Likes.Count);
        Assert.Equal(likerId, comment.Likes.First());
    }

    [Fact]
    public void LikeCommentTwice_LikesShouldIncreasedAndDecrease()
    {
        // Arrange
        var text = "comment test";
        var comment = new Comment(text, null, 1, 1);
        var likerId = 5;

        // Act
        comment.Like(likerId);
        comment.Like(likerId);

        // Assert
        Assert.Equal(0, comment.Likes.Count);
    }

    [Fact]
    public void DislikeComment_DislikesShouldIncreased()
    {
        // Arrange
        var text = "comment test";
        var comment = new Comment(text, null, 1, 1);
        var likerId = 5;

        // Act
        comment.Dislike(likerId);

        // Assert
        Assert.Equal(1, comment.Dislikes.Count);
        Assert.Equal(likerId, comment.Dislikes.First());
    }

    [Fact]
    public void DislikeCommentTwice_DislikesShouldIncreasedAndDecrease()
    {
        // Arrange
        var text = "comment test";
        var comment = new Comment(text, null, 1, 1);
        var likerId = 5;

        // Act
        comment.Dislike(likerId);
        comment.Dislike(likerId);

        // Assert
        Assert.Equal(0, comment.Dislikes.Count);
    }
}
