using Blog.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.EntityTypeConfiguration;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.Property<int>(p => p.Id).ValueGeneratedNever();

        builder.Property(e=> e.Bio)
            .HasMaxLength(500);
        
        builder.Property(e=>e.BirthPlace).HasMaxLength(50);
        
        builder.Property(e=>e.Gender).HasColumnType("tinyint");

        builder.Property(e=>e.FullName).HasMaxLength(50);
    }
}
