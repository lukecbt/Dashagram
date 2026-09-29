namespace Dashagram.Application.DTOs.Posts
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<PostImageDto> Images { get; set; }
    }
}
