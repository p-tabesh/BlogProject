using Blog.Application.Model.Article;

namespace Blog.Application.Mapper.Article;

public class ArticleMapper
{
    //public static ArticleViewModel MapToModel(Domain.Entity.Article article)
    //{
    //    var model = new ArticleViewModel(article.Id,
    //        article.Header,
    //        article.Title,
    //        article.Text,
    //        article.Tags,
    //        article.PublishDate,
    //        article.Status.ToString(),
    //        article?.Likes,
    //        article?.Dislikes,
    //        article.AuthorUserId,
    //        article.CategoryId,
    //        article.PreviewImageLink);
    //    return model;
    //}

    //public static IEnumerable<ArticleViewModel> MapToModel(IEnumerable<Domain.Entity.Article> articles)
    //{
    //    foreach (var article in articles)
    //    {
    //        var model = new ArticleViewModel(article.Id,
    //        article.Header,
    //        article.Title,
    //        article.Text,
    //        article.Tags,
    //        article.PublishDate,
    //        article.Status.ToString(),
    //        article?.Likes,
    //        article?.Dislikes,
    //        article.AuthorUserId,
    //        article.CategoryId,
    //        article.PreviewImageLink);
    //        yield return model;
    //    }
    //}

    public static TDest MapToModel<TSource, TDest>(TSource sourceObject)
        where TSource : class 
        where TDest : class, new()
    {
        if (sourceObject == null)
            throw new ArgumentNullException(nameof(sourceObject));

        var destinationObject = new TDest();
        var destinationProps = typeof(TDest).GetProperties();
        var sourceProps = typeof(TSource).GetProperties();
        foreach (var destinationProp in destinationProps)
        {
            foreach (var sourceProp in sourceProps)
            {
                if (destinationProp.Name == sourceProp.Name &&
                    destinationProp.PropertyType == sourceProp.PropertyType)
                {
                    var value = sourceProp.GetValue(sourceObject);
                    destinationProp.SetValue(destinationObject, value);
                }
            }
        }
        return destinationObject;
    }
}
