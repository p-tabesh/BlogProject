using Blog.Domain.Entity;
using Core.Repository.Model.Specifications;
using System.Linq.Expressions;

namespace Blog.Domain.Specifications;

public class PublishedArticleSpecification : Specification<Article>
{
    public override Expression<Func<Article, bool>> Expression => (article => article.Status == Enum.Status.Published &&
                                                                              article.PublishDate <= DateTime.UtcNow);
}
