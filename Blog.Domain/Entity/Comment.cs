using Blog.Domain.Enum;

namespace Blog.Domain.Entity;

public class Comment : RootEntity<int>
{
    public string Text { get; private set; }
    public List<int> Likes { get; private set; } = new();
    public List<int> Dislikes { get; private set; } = new();
    public bool IsDeleted { get; private set; } = false;
    public bool IsShow { get; private set; } = false;
    public DateTime CreationDate { get; private set; }
    public int UserId { get; private set; }
    public int ArticleId { get; private set; }
    public int? RelatedCommentId { get; private set; }
    public List<Comment>? ChildrenComments { get; private set; }
    public Comment? RelatedComment { get; private set; }

    private Comment() 
    {
        ChildrenComments = new List<Comment>();
    }

    public Comment(string text, int? relatedCommentId, int articleId, int userId)
    {
        Text = !string.IsNullOrEmpty(text) ? text : throw new ArgumentException("Invalid Text");
        ArticleId = articleId;
        RelatedCommentId = relatedCommentId;
        UserId = userId;
        CreationDate = DateTime.UtcNow;
    }

    public void ReplyComment(string text, int userId)
    {
        var comment = new Comment(text, this.Id, this.ArticleId, userId);

        if (ChildrenComments == null)
            ChildrenComments = new List<Comment>();

        ChildrenComments.Add(comment);
    }

    public void Show()
    {
        if (IsShow == false)
            return;

        this.IsShow = true;
    }

    public void Reject()
    {
        if (IsShow == false)
            return;

        this.IsShow = false;
    }
    

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
