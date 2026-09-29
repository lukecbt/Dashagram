using Dashagram.Domain.Models.Entities;

namespace Dashagram.Application.Common.Interfaces.Repositories
{
    public interface IPostLikeRepository : IBaseRepository
    {
        Task<int> CountAsync(Guid postId, CancellationToken cancellationToken = default);
        Task<PostLike> CreatePostLikeAsync(PostLike postLike, CancellationToken cancellationToken = default);
        Task DeletePostLikeAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
