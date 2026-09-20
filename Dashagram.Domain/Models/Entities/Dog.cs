using System.ComponentModel.DataAnnotations.Schema;

namespace Dashagram.Domain.Models.Entities
{
    public record Dog : Entity
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; }

        [ForeignKey("OwnerId")]
        public string OwnerId { get; set; }

        [ForeignKey("BreedId")]
        public int BreedId { get; set; }
        public Breed Breed { get; set; }
    }
}
