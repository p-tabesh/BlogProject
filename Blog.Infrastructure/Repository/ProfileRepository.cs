using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repository;

public class ProfileRepository : CrudRepository<Profile>, IProfileRepository
{
    private readonly BlogDbContext _dbContext;
    private readonly DbSet<Profile> _profiles;
    public ProfileRepository(BlogDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
        _profiles = dbContext.Profile;
    }

    public Profile GetByUserId(int userId)
    {
        var profile = _profiles.FirstOrDefault(p => p.Id == userId);
        return profile;
    }
}
