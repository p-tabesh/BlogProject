using Blog.Domain.Enum;

namespace Blog.Domain.Entity;

public class Article : RootEntity<int>
{
    public string Header { get; private set; }
    public string Title { get; private set; }
    public string Text { get; private set; }
    public string Tag { get; private set; }
    public DateTime PublishDate { get; private set; }
    public DateTime CreationDate { get; private set; }
    public Status Status { get; private set; }
    public string LikedBy { get; private set; } // Will saves json
    public int AuthorUserId { get; private set; }
    public int CategoryId { get; private set; }
}
