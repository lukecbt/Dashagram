using Dashagram.Domain.Models.Entities;

namespace Dashagram.Application.DTOs.Posts
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public ICollection<PostImageDto> Images { get; set; }

        public string UserId { get; set; }
        //public ICollection<PostCommentDto> Comments { get; set; }
        //public ICollection<PostLikeDto> Likes { get; set; }
    }
}
