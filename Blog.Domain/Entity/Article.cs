using Blog.Domain.Enum;

namespace Blog.Domain.Entity;

public class Article : RootEntity<int>
{
    public string Header { get; private set; }
    public string Title { get; private set; }
    public string Text { get; private set; }
    public List<string> Tags { get; private set; } = new();
    public DateTime PublishDate { get; private set; }
    public DateTime CreationDate { get; private set; }
    public Status Status { get; private set; }
    public List<int>? Likes { get; private set; } = new();
    public List<int>? Dislikes { get; private set; } = new();
    public long Views { get; private set; } = 0;
    public int AuthorUserId { get; private set; }
    public int CategoryId { get; private set; }
    public string PreviewImageLink { get; private set; }

    private Article() { }

    public Article(string header, string title, string text, List<string> tags, string previewImageLink, DateTime? publishDate, int authorUserId, int categoryId)
    {
        Header = header;
        Title = title;
        Text = text;
        Tags = tags;
        PreviewImageLink = previewImageLink;
        PublishDate = publishDate.HasValue? publishDate.Value : DateTime.UtcNow;
        AuthorUserId = authorUserId;
        CategoryId = categoryId;
        CreationDate = DateTime.UtcNow;
        Status = Status.Draft;
    }


    
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

    public void Accept()
    {
        Status = Status.Published;

        if (PublishDate == null)
            PublishDate = DateTime.UtcNow;
    }

    public void Reject()
    {
        Status = Status.Rejected;
    }

    public void Edit(string newHeader, string newTitle, string newText, string newPreviewImageLink)
    {
        Header = newHeader;
        Title = newTitle;
        Text = newText;
        PreviewImageLink = newPreviewImageLink;
    }

    public void AddView(int viewCount)
    {
        Views += viewCount;
    }
}
