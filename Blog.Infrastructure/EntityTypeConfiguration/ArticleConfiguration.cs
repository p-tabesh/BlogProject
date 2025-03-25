using Blog.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Text.Json;

namespace Blog.Infrastructure.EntityTypeConfiguration;

class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(e => e.Id);


        builder.Property(a => a.Likes)
        .HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => JsonSerializer.Deserialize<List<int>>(v, (JsonSerializerOptions?)null) ?? new List<int>()
        );

        builder.Property(a => a.Dislikes)
        .HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => JsonSerializer.Deserialize<List<int>>(v, (JsonSerializerOptions?)null) ?? new List<int>()
        );

        builder.Property(a => a.Tags)
        .HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
        );

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.AuthorUserId);

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(e => e.CategoryId);

        builder.HasMany(e => e.FavoritedBy)
            .WithMany()
            .UsingEntity("UserFavoriteArticle");

        builder.HasMany<Comment>()
            .WithOne()
            .HasForeignKey(e=> e.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
