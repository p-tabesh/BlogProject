using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Infrastructure.Context;

namespace Blog.Infrastructure.Repository;

public class CategoryRepository : CrudRepository<Category>, ICategoryRepository
{
    public CategoryRepository(BlogDbContext dbContext)
        : base(dbContext, x => x.ChildCategories) { }
}
