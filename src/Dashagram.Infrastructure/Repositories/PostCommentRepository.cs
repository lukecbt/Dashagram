using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Domain.Models.Entities;
using Dashagram.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Dashagram.Infrastructure.Repositories
{
    public class PostCommentRepository(ApplicationDbContext context) : IPostCommentRepository
    {
        public async Task<IEnumerable<PostComment>> GetByPostIdAsync(Guid postId, CancellationToken cancellationToken = default)
        {
            return await context.PostComments.Where(c => c.PostId == postId).ToListAsync(cancellationToken);
        }

        public async Task<PostComment> CreatePostCommentAsync(PostComment postComment, CancellationToken cancellationToken = default)
        {
            await context.PostComments.AddAsync(postComment, cancellationToken);
            return postComment;
        }

        public async Task DeletePostCommentAsync(Guid id, CancellationToken cancellationToken = default)
        {
            PostComment? comment = await context.PostComments.FindAsync([id], cancellationToken);
            if (comment == null) return;
            context.PostComments.Remove(comment);
            return;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
