using Blog.Domain.IRepository;
using Blog.Infrastructure.Context;
using Blog.Infrastructure.Extention;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.Infrastructure.Repository;

public class CrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity : class
{
    public DbSet<TEntity> Entities { get; init; }

    public CrudRepository(BlogDbContext dbContext)
    {
        Entities = dbContext.Set<TEntity>();
    }

    public CrudRepository(BlogDbContext dbContext, params Expression<Func<TEntity, object>>[] includes)
    {
        Entities = dbContext.Set<TEntity>();
        Entities.IncludeMultiple(includes);
    }

    public void Add(TEntity entity)
    {
        Entities.Add(entity);
    }

    public IEnumerable<TEntity> GetAll()
    {
        var entities = Entities.ToList();
        return entities;
    }

    public TEntity GetById(int id)
    {
        var entity = Entities.Find(id);
        return entity;
    }

    public void Remove(TEntity entity)
    {
        Entities.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        Entities.Update(entity);
    }
}
