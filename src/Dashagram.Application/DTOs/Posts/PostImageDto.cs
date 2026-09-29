namespace Dashagram.Application.DTOs.Posts
{
    public class PostImageDto
    {
        public PostImageDto(string url)
        {
            Url = url;
        }

        public string Url { get; set; }
    }
}
