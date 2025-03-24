using Blog.Infrastructure.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Context;

class BlogDbContext : DbContext
{
    public BlogDbContext() { }
    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ArticleConfiguration());
    }
}
