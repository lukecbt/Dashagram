using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Models;
using Dashagram.Application.DTOs.Posts;
using Dashagram.Domain.Models.Entities;
using MediatR;

namespace Dashagram.Application.Features.Posts.Queries
{
    public record GetAllPostsByUserIdQuery(string UserId) : IRequest<Result<IEnumerable<PostDto>>>;

    public class GetAllPostsByUserIdQueryHandler(IPostRepository repository) : IRequestHandler<GetAllPostsByUserIdQuery, Result<IEnumerable<PostDto>>>
    {
        public async Task<Result<IEnumerable<PostDto>>> Handle(GetAllPostsByUserIdQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Post> posts = await repository.GetAllPostsByUserIdAsync(request.UserId, cancellationToken);

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
