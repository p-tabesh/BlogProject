using Blog.Application.Model.Article;
using Blog.Domain.Specifications;
using Core.Repository.Model.Specifications;

namespace Blog.Application.Service;

public class SpecificationBuilder
{
    public static Specification<Domain.Entity.Article> GetFilterSpecifications(ArticleFilterModel filter)
    {
        var specification = new TrueSpecification<Domain.Entity.Article>();

        specification.And(new ArticleCategorySpecification(filter.CategoryId))
            .And(new ArticleAuthorUserSpecification(filter.AuthorUserId))
            .And(new ArticlePublisDateSpecification(filter.startPublishDate, filter.endPublishDate))
            .And(new PublishedArticleSpecification());

        return specification;
    }
}
