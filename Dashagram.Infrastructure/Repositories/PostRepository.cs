using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Domain.Models.Entities;
using Dashagram.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dashagram.Infrastructure.Repositories
{
    public class PostRepository(ApplicationDbContext context) : IPostRepository
    {
        public async Task<Post> CreatePostAsync(Post post, CancellationToken cancellationToken = default)
        {
            await context.Posts.AddAsync(post, cancellationToken);
            return post;
        }

        public async Task DeletePostAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Post? post = await context.Posts.FindAsync([id], cancellationToken);
            if (post == null) return;
            context.Posts.Remove(post);
            return;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync(int page, int size, CancellationToken cancellationToken = default)
        {
            return await context.Posts
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * size)
                .Take(size)
                .Include(p => p.Images)
                .ToListAsync(cancellationToken);
        }

        public async Task<Post?> GetPostByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Posts
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Post>> GetAllPostsByUserIdAsync(string userId, int page, int size, CancellationToken cancellationToken = default)
        {
            return await context.Posts
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * size)
                .Take(size)
                .Include(p => p.Images)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> CountAsync(string? userId, CancellationToken cancellationToken = default)
        {
            IQueryable<Post> query = context.Posts;

            if (userId is not null)
            {
                query = query.Where(p => p.UserId == userId);
            }

            return await query.CountAsync(cancellationToken);
        }
    }
}
