using Blog.Domain.Enum;

namespace Blog.Domain.Entity;

public class Comment : RootEntity<int>
{
    public string Text { get; private set; }
    public Rate? Rate { get; private set; }
    public List<int> Likes { get; private set; } = new();
    public List<int> Dislikes { get; private set; } = new();
    public bool IsDeleted { get; private set; } = false;
    public DateTime CreationDate { get; private set; }
    public int UserId { get; private set; }
    public int? ArticleId { get; private set; }
    public int? RelatedCommentId { get; private set; }
    public List<Comment>? ChildrenComments { get; private set; }
    public Comment? RelatedComment { get; private set; }

    private Comment() { }

    private Comment(string text, int userId, Rate? rate = null, int? articleId = null, int? relatedCommentId = null)
    {
        Text = string.IsNullOrEmpty(text) ? text : throw new ArgumentException("Invalid Text");
        ArticleId = articleId;
        RelatedCommentId = relatedCommentId;
        UserId = userId;
        Rate = rate;
        CreationDate = DateTime.UtcNow;
    }


    #region Creation
    public static Comment Create(string text, int userId, int articleId, Rate rate)
    {
        var comment = new Comment(text,userId, rate, articleId);
        return comment;
    }
    

    public static Comment CreateReply(string text, int userId, int relatedCommentId)
    {
        var comment = new Comment(text, userId, relatedCommentId: relatedCommentId);
        return comment;
    }
    #endregion

    #region Likes
    public void Like(int userId)
    {
        if (Likes.Any(x => x == userId))
            Likes.Remove(userId);

        else
            Likes.Add(userId);
    }

    public void Dislike(int userId)
    {
        if (Dislikes.Any(x => x == userId))
            Dislikes.Remove(userId);

        else
            Dislikes.Add(userId);
    }
    #endregion


}
