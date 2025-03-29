using Blog.Domain.Entity;
using Blog.Infrastructure.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Context;

public class BlogDbContext : DbContext
{
    public DbSet<Article> Article { get; set; }
    //public DbSet<Category> Category { get; set; }
    //public DbSet<Comment> Comment { get; set; }
    //public DbSet<Profile> Profile { get; set; }
    //public DbSet<User> User { get; set; }

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
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new ProfileConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
