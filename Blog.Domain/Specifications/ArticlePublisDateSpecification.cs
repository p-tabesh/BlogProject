using Blog.Domain.Entity;
using Core.Repository.Model.Specifications;
using System.Linq.Expressions;

namespace Blog.Domain.Specifications;

public class ArticlePublisDateSpecification : Specification<Article>
{
    private DateTime? _start;
    private DateTime? _end;

    public ArticlePublisDateSpecification(DateTime? startDate, DateTime? endDate)
    {
        _start = startDate;
        _end = endDate;
    }

    protected override bool IsApplicable => _start.HasValue && _end.HasValue;

    public override Expression<Func<Article, bool>> Expression => article => article.PublishDate >= _start && article.PublishDate<=_end;
}
