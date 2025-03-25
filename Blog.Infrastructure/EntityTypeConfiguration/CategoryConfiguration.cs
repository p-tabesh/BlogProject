using Blog.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.EntityTypeConfiguration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(e => e.ChildCategories)
            .WithOne(e => e.ParentCategory)
            .HasForeignKey(e=>e.ParentCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
