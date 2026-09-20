using Dashagram.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dashagram.Infrastructure.Configurations
{
    public class BreedConfiguation : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(100);

            // Seed data for the Breed entity
            builder.HasData(
                new Breed { Id = 1, Name = "Labrador Retriever", CreatedAt = DateTime.MinValue },
                new Breed { Id = 2, Name = "Golden Retriever", CreatedAt = DateTime.MinValue },
                new Breed { Id = 3, Name = "German Shepherd", CreatedAt = DateTime.MinValue },
                new Breed { Id = 4, Name = "French Bulldog", CreatedAt = DateTime.MinValue },
                new Breed { Id = 5, Name = "Poodle", CreatedAt = DateTime.MinValue },
                new Breed { Id = 6, Name = "Bulldog", CreatedAt = DateTime.MinValue },
                new Breed { Id = 7, Name = "Beagle", CreatedAt = DateTime.MinValue },
                new Breed { Id = 8, Name = "Rottweiler", CreatedAt = DateTime.MinValue },
                new Breed { Id = 9, Name = "Dachshund", CreatedAt = DateTime.MinValue },
                new Breed { Id = 10, Name = "Yorkshire Terrier", CreatedAt = DateTime.MinValue },
                new Breed { Id = 11, Name = "Boxer", CreatedAt = DateTime.MinValue },
                new Breed { Id = 12, Name = "Siberian Husky", CreatedAt = DateTime.MinValue },
                new Breed { Id = 13, Name = "Australian Shepherd", CreatedAt = DateTime.MinValue },
                new Breed { Id = 14, Name = "Shih Tzu", CreatedAt = DateTime.MinValue },
                new Breed { Id = 15, Name = "Cavalier King Charles Spaniel", CreatedAt = DateTime.MinValue },
                new Breed { Id = 16, Name = "Doberman Pinscher", CreatedAt = DateTime.MinValue },
                new Breed { Id = 17, Name = "Great Dane", CreatedAt = DateTime.MinValue },
                new Breed { Id = 18, Name = "Miniature Schnauzer", CreatedAt = DateTime.MinValue },
                new Breed { Id = 19, Name = "Pembroke Welsh Corgi", CreatedAt = DateTime.MinValue },
                new Breed { Id = 20, Name = "Border Collie", CreatedAt = DateTime.MinValue },
                new Breed { Id = 21, Name = "Bernese Mountain Dog", CreatedAt = DateTime.MinValue },
                new Breed { Id = 22, Name = "Pomeranian", CreatedAt = DateTime.MinValue },
                new Breed { Id = 23, Name = "Havanese", CreatedAt = DateTime.MinValue },
                new Breed { Id = 24, Name = "Shetland Sheepdog", CreatedAt = DateTime.MinValue },
                new Breed { Id = 25, Name = "Boston Terrier", CreatedAt = DateTime.MinValue },
                new Breed { Id = 26, Name = "English Springer Spaniel", CreatedAt = DateTime.MinValue },
                new Breed { Id = 27, Name = "Maltese", CreatedAt = DateTime.MinValue },
                new Breed { Id = 28, Name = "Bichon Frise", CreatedAt = DateTime.MinValue },
                new Breed { Id = 29, Name = "Chihuahua", CreatedAt = DateTime.MinValue },
                new Breed { Id = 30, Name = "Cane Corso", CreatedAt = DateTime.MinValue }
            );
        }
    }
}
