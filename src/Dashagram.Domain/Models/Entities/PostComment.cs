namespace Dashagram.Domain.Models.Entities
{
    public record PostComment : Entity
    {
        public string Content { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public string UserId { get; set; }
    }
}
