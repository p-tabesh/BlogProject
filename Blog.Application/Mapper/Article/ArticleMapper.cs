using Blog.Application.Model.Article;

namespace Blog.Application.Mapper.Article;

public class ArticleMapper
{
    public static ArticleViewModel MapToModel(Domain.Entity.Article article)
    {
        var model = new ArticleViewModel(article.Id,
            article.Header,
            article.Title,
            article.Text,
            article.Tags,
            article.PublishDate,
            article.Status.ToString(),
            article?.Likes,
            article?.Dislikes,
            article.AuthorUserId,
            article.CategoryId,
            article.PreviewImageLink);
        return model;
    }

    public static IEnumerable<ArticleViewModel> MapToModel(IEnumerable<Domain.Entity.Article> articles)
    {
        foreach (var article in articles)
        {
            var model = new ArticleViewModel(article.Id,
            article.Header,
            article.Title,
            article.Text,
            article.Tags,
            article.PublishDate,
            article.Status.ToString(),
            article?.Likes,
            article?.Dislikes,
            article.AuthorUserId,
            article.CategoryId,
            article.PreviewImageLink);
            yield return model;
        }
    }
}
