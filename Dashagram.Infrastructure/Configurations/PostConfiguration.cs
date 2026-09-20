using Dashagram.Domain.Models.Entities;
using Dashagram.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dashagram.Infrastructure.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.UserId).IsRequired();

            builder.HasOne<ApplicationUser>()
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the relationships with PostImage, PostComment, and PostLike entities
            builder.HasMany(p => p.Images)
                .WithOne(p => p.Post)
                .HasForeignKey(pi => pi.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Likes)
                .WithOne(l => l.Post)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
