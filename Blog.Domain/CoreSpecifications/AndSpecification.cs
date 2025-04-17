using Blog.Domain.Entity;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace Core.Repository.Model.Specifications;

public class AndSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    public AndSpecification(Specification<T> left, Specification<T> right)
    {
        _right = right;
        _left = left;
    }

    public override Expression<Func<T, bool>> Expression => _left.Expression.And(_right.Expression);
}