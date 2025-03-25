using Blog.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.EntityTypeConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(e => e.Username)
            .Property(p => p.Value)
            .HasColumnName("Username");

        builder.OwnsOne(e => e.Password)
            .Property(p => p.Value)
            .HasColumnName("Password");

        builder.OwnsOne(e => e.Email)
            .Property(p => p.Value)
            .HasColumnName("Email");

        builder.HasMany<Article>()
            .WithMany()
            .UsingEntity("UserFavoriteArticle");
    }
}
