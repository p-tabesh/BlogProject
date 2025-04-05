namespace Blog.Application.Model.Article;

public record UpdateArticleRequest(int ArticleId, string NewHeader, string NewTitle, string NewText, string NewPreviewImageLink);

