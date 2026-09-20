namespace Dashagram.Domain.Models.Entities
{
    public record Breed : Entity
    {
        public new int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Dog> Dogs { get; set; } = [];
    }
}
