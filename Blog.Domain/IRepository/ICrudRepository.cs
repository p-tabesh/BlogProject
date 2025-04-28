using Core.Repository.Model.Specifications;

namespace Blog.Domain.IRepository;

public interface ICrudRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    TEntity GetById(int id);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetWithSpecification(Specification<TEntity> specification);
}
