using Blog.Domain.Entity;
using Core.Repository.Model.Specifications;
using System.Linq.Expressions;

namespace Blog.Domain.Specifications;

public class RecentArticleSpecification : Specification<Article>
{
    public override Expression<Func<Article, bool>> Expression => article => article.PublishDate >= DateTime.UtcNow.AddDays(-2) &&
                                                                                                    article.Status == Enum.Status.Published;
}
