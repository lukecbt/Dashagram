namespace Dashagram.Domain.Models.Entities
{
    public record PostLike : Entity
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Guid UserId { get; set; }
    }
}
