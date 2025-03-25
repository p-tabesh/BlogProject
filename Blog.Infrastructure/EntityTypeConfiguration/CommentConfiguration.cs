using Blog.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.EntityTypeConfiguration;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasMany(e => e.ChildrenComments)
            .WithOne(e => e.RelatedComment)
            .HasForeignKey(e => e.RelatedCommentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
