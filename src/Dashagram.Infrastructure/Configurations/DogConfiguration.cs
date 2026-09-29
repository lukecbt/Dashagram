using Dashagram.Domain.Models.Entities;
using Dashagram.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dashagram.Infrastructure.Configurations
{
    public class DogConfiguration : IEntityTypeConfiguration<Dog>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Dog> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(100);
            builder.Property(d => d.DateOfBirth).IsRequired();
            builder.Property(d => d.Bio).HasMaxLength(500);
            
            builder.HasOne(d => d.Breed)
                .WithMany(b => b.Dogs)
                .HasForeignKey(d => d.BreedId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ApplicationUser>()
                .WithMany(u => u.Dogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
