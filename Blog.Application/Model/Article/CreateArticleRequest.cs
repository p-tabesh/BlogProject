namespace Blog.Application.Model.Article;

public record CreateArticleRequest(
    string Header,
    string Title,
    string Text,
    List<string> Tags,
    DateTime? PublishDate,
    string PreviewImageLink,
    int categoryId
    );

