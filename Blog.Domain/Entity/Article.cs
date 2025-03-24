using Blog.Domain.Enum;
using System.Text.Json;

namespace Blog.Domain.Entity;

public class Article : RootEntity<int>
{
    public string Header { get; private set; }
    public string Title { get; private set; }
    public string Text { get; private set; }
    //public List<string> Tags { get; private set; } // Will saves json maybe
    public DateTime? PublishDate { get; private set; }
    public DateTime CreationDate { get; private set; }
    public Status Status { get; private set; }
    public List<int>? Likes { get; private set; } // Will saves json
    //public List<int> Dislikes { get; private set; } // Will saves json
    public int AuthorUserId { get; private set; }
    public int CategoryId { get; private set; }

    private Article() { }

    public Article(string header, string title, string text, List<string> tags, DateTime? publishDate, int authorUserId, int categoryId)
    {
        Header = header;
        Title = title;
        Text = text;
        //Tags = tags;
        PublishDate = publishDate;
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
    //public void Dislike(int userId)
    //{
    //    if (Dislikes.Any(x => x == userId))
    //        Dislikes.Remove(userId);

    //    else
    //        Dislikes.Add(userId);
    //}

    public void Accept()
    {
        Status = Status.Accepted;

        if (PublishDate == null)
            PublishDate = DateTime.UtcNow;
    }

    public void Reject()
    {
        Status = Status.Rejected;
    }

    public void EditText(string newText)
    {
        Text = newText;
    }
}
