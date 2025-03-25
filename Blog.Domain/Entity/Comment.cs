using Blog.Domain.Enum;

namespace Blog.Domain.Entity;

public class Comment : RootEntity<int>
{
    public string Text { get; private set; }
    public Rate Rate { get; private set; }
    public int Likes { get; private set; }
    public int DisLikes { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreationDate { get; private set; }
    public int UserId { get; private set; }
    public int ArticleId { get; private set; }
    public int? RelatedCommentId { get; private set; }
    public List<Comment> ChildrenComment { get; private set; }
    public Comment RelatedComment { get; private set; }
}
