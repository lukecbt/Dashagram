using Dashagram.Application.Common.Errors;
using Dashagram.Application.Common.Helpers;
using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Models;
using Dashagram.Application.DTOs.Posts;
using Dashagram.Domain.Models.Entities;
using MediatR;

namespace Dashagram.Application.Features.Posts.Queries
{
    public record GetPostByIdQuery(Guid Id) : IRequest<Result<PostDto?>>;

    public class GetPostByIdQueryHandler(IPostRepository repository) : IRequestHandler<GetPostByIdQuery, Result<PostDto?>>
    {
        public async Task<Result<PostDto?>> Handle (GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            Post? post = await repository.GetPostByIdAsync(request.Id, cancellationToken);

            if (post is null)
            {
                return Result<PostDto?>.Fail(ErrorCodes.NotFound, [new Error
                { 
                    Details = ErrorHelper.NotFound("Post", request.Id),
                    Message = ErrorHelper.NotFound("Post")
                }]);
            }

            // TODO: need to get the post with the user photo url, name, comments and likes

            return Result<PostDto?>.Ok(new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                UserId = post.UserId,
                Images = [.. post.Images?.Select(i => new PostImageDto(i.Url)) ?? []]
            });
        }
    }
}
