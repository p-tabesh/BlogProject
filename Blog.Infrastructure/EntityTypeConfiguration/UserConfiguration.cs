using Blog.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

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

        builder.Property(a => a.FavoriteArticleIds)
        .HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => JsonSerializer.Deserialize<List<int>>(v, (JsonSerializerOptions?)null) ?? new List<int>()
        );

        builder.HasMany<Comment>()
            .WithOne()
            .HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.Profile)
            .WithOne()
            .HasForeignKey<Profile>(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade);   
    }
}
