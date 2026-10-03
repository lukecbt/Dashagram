namespace Dashagram.Domain.Models.Entities
{
    public record Post : Entity
    {
        public string Description { get; set; }
        public ICollection<PostImage> Images { get; set; }
        
        public string UserId { get; set; }
        public ICollection<PostComment> Comments { get; set; }
        public ICollection<PostLike> Likes { get; set; }
        // TODO: Add other properties later, such as linked dogs
    }
}
