using Dashagram.Domain.Models.Entities;

namespace Dashagram.Application.Common.Interfaces.Repositories
{
    public interface IPostRepository : IBaseRepository
    {
        Task<Post?> GetPostByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Post>> GetAllPostsAsync(int page, int size, CancellationToken cancellationToken = default);
        Task<IEnumerable<Post>> GetAllPostsByUserIdAsync(string userId, int page, int pageSize, CancellationToken cancellationToken = default);
        Task<IEnumerable<PostImage>> GetAllPostImagesByPostId(Guid postId, CancellationToken cancellationToken = default);
        Task<Post> CreatePostAsync(Post post, CancellationToken cancellationToken = default);
        Task DeletePostAsync(Post post, CancellationToken cancellationToken = default);
        Task<int> CountAsync(string? userId, CancellationToken cancellationToken = default);
    }
}
