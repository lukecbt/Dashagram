namespace Dashagram.Application.DTOs.Posts
{
    public class PostCommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
