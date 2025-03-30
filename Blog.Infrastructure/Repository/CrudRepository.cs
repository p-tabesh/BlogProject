using Blog.Domain.IRepository;
using Blog.Infrastructure.Context;
using Blog.Infrastructure.Extention;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq.Expressions;

namespace Blog.Infrastructure.Repository;

public class CrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity : class
{
    private readonly BlogDbContext _dbContext;
    private readonly DbSet<TEntity> _entities;
    public DbSet<TEntity> Entities { get; init; }
    public CrudRepository(BlogDbContext dbContext)
    {
        Entities = dbContext.Set<TEntity>();
        _dbContext = dbContext;       
        _entities = dbContext.Set<TEntity>();
    }
    public CrudRepository(BlogDbContext dbContext, params Expression<Func<TEntity, object>>[] includes)
    {
        _dbContext = dbContext;
        _entities = dbContext.Set<TEntity>();
        _entities.IncludeMultiple(includes);
    }
    public void Add(TEntity entity)
    {
        _dbContext.Add(entity);
    }

    public IEnumerable<TEntity> GetAll()
    {
        var entities = _entities.ToList();
        return entities;
    }

    public TEntity GetById(int id)
    {
        var entity = _dbContext.Set<TEntity>().Find(id);
        return entity;
    }

    public void Remove(TEntity entity)
    {
        _dbContext.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Update(entity);
    }
}
