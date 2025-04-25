using Blog.Domain.IUnitOfWork;
using Blog.Infrastructure.Context;

namespace Blog.Infrastructure.UnitOfWork;

public class UnitOfWork(BlogDbContext dbContext) : IUnitOfWork
{
    private readonly BlogDbContext _dbContext = dbContext;

    public void Commit()
    {
        _dbContext.SaveChanges();
    }

    public void Rollback()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
        GC.SuppressFinalize(this);
    }
}
