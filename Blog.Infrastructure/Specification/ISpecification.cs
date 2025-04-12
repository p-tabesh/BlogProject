namespace Blog.Infrastructure.Specification;

public interface ISpecification<TEntity> where TEntity : class
{
    bool IsSatisfiedBy(TEntity entity);
}
