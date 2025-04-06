using Blog.Domain.Entity;

namespace Blog.Domain.IRepository;

public interface IProfileRepository : ICrudRepository<Profile>
{
    Profile GetByUserId(int userId);
}
