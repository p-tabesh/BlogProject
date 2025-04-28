using Blog.Domain.Entity;
using Core.Repository.Model.Specifications;
using System.Linq.Expressions;

namespace Blog.Domain.Specifications;

public class ArticleAuthorUserSpecification : Specification<Article>
{
    private int? _authorUserId;
    public ArticleAuthorUserSpecification(int? authorUserId)
    {
        _authorUserId = authorUserId;
    }

    protected override bool IsApplicable => _authorUserId.HasValue;
    public override Expression<Func<Article, bool>> Expression => article => article.AuthorUserId == _authorUserId;
}
