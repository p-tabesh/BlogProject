using System.Linq.Expressions;
using LinqExpression = System.Linq.Expressions.Expression;

namespace Core.Repository.Model.Specifications;

public class TrueSpecification<T> : Specification<T>
{
    public override Expression<Func<T, bool>> Expression => PredicateBuilder.True<T>();
}