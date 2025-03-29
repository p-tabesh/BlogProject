using Blog.Domain.IRepository;
using Blog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repository;

public class CrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity : class
{
    private readonly BlogDbContext _dbContext;
    public CrudRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Add(TEntity entity)
    {
        _dbContext.Add(entity);
    }

    public void Remove(TEntity entity)
    {
        _dbContext.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Update(entity);
    }

    public IEnumerable<TEntity> GetAll()
    {
        var entities = _dbContext.Set<TEntity>().ToList();
        return entities;
    }

    public TEntity GetById(int id)
    {
        var entity = _dbContext.Set<TEntity>().Find(id);
        return entity;
    }


}
