using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Domain.Models.Entities;
using Dashagram.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Dashagram.Infrastructure.Repositories
{
    public class PostLikeRepository(ApplicationDbContext context) : IPostLikeRepository
    {
        public async Task<int> CountAsync(Guid postId, CancellationToken cancellationToken = default)
        {
            return await context.PostLikes.CountAsync(cancellationToken);
        }

        public async Task<PostLike> CreatePostLikeAsync(PostLike postLike, CancellationToken cancellationToken = default)
        {
            await context.PostLikes.AddAsync(postLike, cancellationToken);
            return postLike;
        }

        public async Task DeletePostLikeAsync(Guid id, CancellationToken cancellationToken = default)
        {
            PostLike? postLike = await context.PostLikes.FindAsync([id], cancellationToken);
            if (postLike == null) return;
            context.PostLikes.Remove(postLike);
            return;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
