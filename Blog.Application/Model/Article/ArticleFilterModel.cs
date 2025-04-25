namespace Blog.Application.Model.Article;

public record ArticleFilterModel(int? CategoryId, int? AuthorUserId, string? Tag);
