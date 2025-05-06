namespace Blog.Application.Model.Article;

public record ArticleViewEventModel(string ConnectionId,int ArticleId, DateTime ViewTime);
