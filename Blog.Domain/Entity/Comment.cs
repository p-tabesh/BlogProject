namespace Blog.Domain.Entity;

public class Comment : RootEntity<int>
{
    public string Text { get; private set; }
    public int? RelatedCommentId { get; private set; }
    public DateTime CreationDate { get; private set; }
    public bool IsDeleted { get; private set; }
    public int UserId { get; private set; }
    public int ArticleId { get; private set; }
}
