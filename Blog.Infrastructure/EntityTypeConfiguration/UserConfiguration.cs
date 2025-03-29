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
            .HasColumnName("Username")
            .HasMaxLength(50);

        builder.OwnsOne(e => e.Password)
            .Property(p => p.Value)
            .HasColumnName("Password")
            .HasMaxLength(512);

        builder.OwnsOne(e => e.Email)
            .Property(p => p.Value)
            .HasColumnName("Email")
            .HasMaxLength(50);

        builder.HasMany(e => e.FavoriteArticles)
           .WithMany(e => e.FavoritedBy)
           .UsingEntity("UserFavoriteArticle");

        builder.HasMany<Article>()
            .WithMany()
            .UsingEntity("UserFavoriteArticle");


        builder.HasMany<Comment>()
            .WithOne()
            .HasForeignKey(e => e.UserId);

        builder.HasOne<Profile>(e => e.Profile)
            .WithOne(e=> e.User)
            .HasForeignKey<Profile>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);       
    }
}
