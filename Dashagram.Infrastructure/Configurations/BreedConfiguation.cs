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
                new Breed { Id = 1, Name = "Labrador Retriever" },
                new Breed { Id = 2, Name = "Golden Retriever" },
                new Breed { Id = 3, Name = "German Shepherd" },
                new Breed { Id = 4, Name = "French Bulldog" },
                new Breed { Id = 5, Name = "Poodle" },
                new Breed { Id = 6, Name = "Bulldog" },
                new Breed { Id = 7, Name = "Beagle" },
                new Breed { Id = 8, Name = "Rottweiler" },
                new Breed { Id = 9, Name = "Dachshund" },
                new Breed { Id = 10, Name = "Yorkshire Terrier" },
                new Breed { Id = 11, Name = "Boxer" },
                new Breed { Id = 12, Name = "Siberian Husky" },
                new Breed { Id = 13, Name = "Australian Shepherd" },
                new Breed { Id = 14, Name = "Shih Tzu" },
                new Breed { Id = 15, Name = "Cavalier King Charles Spaniel" },
                new Breed { Id = 16, Name = "Doberman Pinscher" },
                new Breed { Id = 17, Name = "Great Dane" },
                new Breed { Id = 18, Name = "Miniature Schnauzer" },
                new Breed { Id = 19, Name = "Pembroke Welsh Corgi" },
                new Breed { Id = 20, Name = "Border Collie" },
                new Breed { Id = 21, Name = "Bernese Mountain Dog" },
                new Breed { Id = 22, Name = "Pomeranian" },
                new Breed { Id = 23, Name = "Havanese" },
                new Breed { Id = 24, Name = "Shetland Sheepdog" },
                new Breed { Id = 25, Name = "Boston Terrier" },
                new Breed { Id = 26, Name = "English Springer Spaniel" },
                new Breed { Id = 27, Name = "Maltese" },
                new Breed { Id = 28, Name = "Bichon Frise" },
                new Breed { Id = 29, Name = "Chihuahua" },
                new Breed { Id = 30, Name = "Cane Corso" }
            );
        }
    }
}
