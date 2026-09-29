using Dashagram.Domain.Models.Entities;

namespace Dashagram.Application.Common.Interfaces.Repositories
{
    public interface IPostCommentRepository : IBaseRepository
    {
        Task<IEnumerable<PostComment>> GetByPostIdAsync(Guid postId, CancellationToken cancellationToken = default);
        Task<PostComment> CreatePostCommentAsync(PostComment postComment, CancellationToken cancellationToken = default);
        Task DeletePostCommentAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
