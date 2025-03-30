namespace Blog.Domain.IUnitOfWork;

public interface IUnitOfWork : IDisposable
{
    void Commit();
    void Rollback();
}
