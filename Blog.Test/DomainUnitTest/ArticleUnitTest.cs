using Blog.Domain.Entity;
using FluentAssertions;

namespace Blog.Test.Unit;

public class ArticleUnitTest
{


    [Fact]
    public void LikeArticle_IncreaseArticleLikes()
    {
        // Arrange
        var article = new Article("","","",new List<string> { ""},"",DateTime.Now,1,1);
        int likerId = 2;

        // Act
        article.Like(likerId);

        // Assert
        article.Likes.Count.Should().BeGreaterThan(0);
        article.Likes.First().Should().Be(likerId);
    }

    [Fact]
    public void DislikeArticle_IncreaseArticleDislikes()
    {
        // Arrange
        var article = new Article("", "", "", new List<string> { "" }, "", DateTime.Now, 1, 1);
        int dislikerId = 2;

        // Act
        article.Dislike(dislikerId);

        // Assert
        article.Dislikes.Count.Should().BeGreaterThan(0);
        article.Dislikes.First().Should().Be(dislikerId);
    }

    [Fact]
    public void AcceptArticle_StatusShouldBePublished()
    {
        // Arrange
        var article = new Article("", "", "", new List<string> { "" }, "", DateTime.Now, 1, 1);

        // Act
        article.Accept();

        // Assert
        article.Status.Should().Be(Domain.Enum.Status.Published);
    }

    [Fact]
    public void RejectArticle_StatusShouldBeRejected()
    {
        // Arrange 
        var article = new Article("", "", "", new List<string> { "" }, "", DateTime.Now, 1, 1);

        // Act
        article.Reject();

        // Assert
        article.Status.Should().Be(Domain.Enum.Status.Rejected);
    }

    [Fact]
    public void AddView_IncreaseArticleView()
    {
        // Arrange 
        var article = new Article("", "", "", new List<string> { "" }, "", DateTime.Now, 1, 1);
        var viewCount = 2;

        // Act
        article.AddView(viewCount);

        // Assert
        article.Views.Should().BeGreaterThan(0);
        article.Views.Should().Be(viewCount);
    }

    [Fact]
    public void EditArticle_ShouldMatchNewData()
    {
        // Arrange 
        var article = new Article("", "", "", new List<string> { "" }, "", DateTime.Now, 1, 1);
        var newHeader = "New header test";
        var newTitle = "New title test";
        var newText = "New text test";
        var newPreviewImageLink = "New preview link";

        // Act
        article.Edit(newHeader,newTitle,newText,newPreviewImageLink);


        // Assert
        article.Should().BeEquivalentTo(new
        {
            Header = newHeader,
            Title = newTitle,
            Text = newText,
            PreviewImageLink = newPreviewImageLink
        });
    }
}
