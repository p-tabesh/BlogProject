using Blog.Domain.IRepository;
using Blog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq.Expressions;

namespace Blog.Infrastructure.Repository;

public class CrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity : class
{
    private readonly BlogDbContext _dbContext;
    private readonly DbSet<TEntity> _entities;
    public CrudRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
        _entities.Include(t => typeof(TEntity));
        
    }
    public void Add(TEntity entity)
    {
        _dbContext.Add(entity);
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

    public void Remove(TEntity entity)
    {
        _dbContext.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Update(entity);

        Expression<Func<int, int>> expression = (a) => a + 1;
        void salam(Expression<Func<int, int>> ex)
        {
            
        }
        salam(a=>a+2);
        
    }
}
