using System.ComponentModel.DataAnnotations.Schema;

namespace Dashagram.Domain.Models.Entities
{
    public record Dog : Entity
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; }

        public string UserId { get; set; }

        public int BreedId { get; set; }
        public Breed Breed { get; set; }
    }
}
