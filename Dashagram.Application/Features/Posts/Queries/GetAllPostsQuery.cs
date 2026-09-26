using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Models;
using Dashagram.Application.DTOs.Posts;
using Dashagram.Domain.Models.Entities;
using MediatR;

namespace Dashagram.Application.Features.Posts.Queries
{
    public record GetAllPostsQuery(int? Page, int? Size) : IRequest<Result<IEnumerable<PostDto>>>;

    public class GetAllPostsQueryHandler(IPostRepository repository) : IRequestHandler<GetAllPostsQuery, Result<IEnumerable<PostDto>>>
    {
        public async Task<Result<IEnumerable<PostDto>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var page = request.Page ?? 1;
            var size = request.Size ?? 9;

            IEnumerable<Post> posts = await repository.GetAllPostsAsync(page, size, cancellationToken);

            // TODO: need to get the posts with the user photo url, name, comments and likes

            return Result<IEnumerable<PostDto>>.Ok(posts.Select(post => new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                UserId = post.UserId
            }));
        }
    }
}
