namespace Dashagram.Domain.Models.Entities
{
    public abstract record Entity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
