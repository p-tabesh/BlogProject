namespace Blog.Application.Model.Article;

public record EditArticleRequest(int ArticleId, string NewHeader, string NewTitle, string NewText, string NewPreviewImageLink);

