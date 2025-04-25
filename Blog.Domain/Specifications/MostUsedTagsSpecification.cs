using Blog.Domain.Entity;
using Core.Repository.Model.Specifications;
using System.Linq.Expressions;

namespace Blog.Domain.Specifications;

public class MostUsedTagsSpecification : Specification<Article>
{

    public override Expression<Func<Article, bool>> Expression => throw new NotImplementedException();
}
