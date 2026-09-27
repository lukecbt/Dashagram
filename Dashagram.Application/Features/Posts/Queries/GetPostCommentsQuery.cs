using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Models;
using Dashagram.Application.DTOs.Posts;
using MediatR;

namespace Dashagram.Application.Features.Posts.Queries
{
    public record GetPostCommentsQuery(Guid PostId) : IRequest<Result<IEnumerable<PostCommentDto>>>;

    public class GetPostCommentsQueryHandler(IPostCommentRepository repository) : IRequestHandler<GetPostCommentsQuery, Result<IEnumerable<PostCommentDto>>>
    {
        public async Task<Result<IEnumerable<PostCommentDto>>> Handle(GetPostCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await repository.GetByPostIdAsync(request.PostId, cancellationToken);

            var dtos = comments.Select(c => new PostCommentDto
            {
                Id = c.Id,
                Content = c.Content,
                UserId = c.UserId,
                CreatedAt = c.CreatedAt
            });

            return Result<IEnumerable<PostCommentDto>>.Ok(dtos);
        }
    }
}
