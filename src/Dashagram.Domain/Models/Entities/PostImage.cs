namespace Dashagram.Domain.Models.Entities
{
    /// <summary>
    /// Represents an image associated with a post.
    /// </summary>
    public record PostImage : Entity
    {
        public string Url { get; set; }
        public int Order { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
