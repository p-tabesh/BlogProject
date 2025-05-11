namespace Blog.Application.Model.Article;

public record ArticleViewModel(int Id,
        string Header,
        string Title,
        string Text,
        List<string> Tags,
        DateTime PublishDate,
        string Status,
        List<int>? Likes,
        List<int>? Dislikes,
        long Views,
        int AuthorUserId,
        int CategoryId,
        string PreviewImageLink);

//public class ArticleViewModel
//{
//    public ArticleViewModel() { }
//    public string Header { get; set; }
//    public string Title { get; set; }
//    public string Text { get; set; }
//    public List<string> Tags { get; set; }
//    public DateTime PublishDate { get; set; }
//    public string Status { get; set; }
//    public List<int> Likes { get; set; }
//    public List<int> Dislikes { get; set; }
//    public int AuthorUserId { get; set; }
//    public int CategoryId { get; set; }
//    public string PreviewImageLink { get; set; }
//}