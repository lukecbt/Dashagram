using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Models.Response;
using Dashagram.Application.DTOs.Posts;
using Dashagram.Domain.Models.Entities;
using MediatR;

namespace Dashagram.Application.Features.Posts.Commands
{
    public record CreatePostCommentCommand(Guid PostId, string Content, string UserId) : IRequest<Result<PostCommentDto>>;

    public class CreatePostCommentCommandHandler(IPostCommentRepository repository) : IRequestHandler<CreatePostCommentCommand, Result<PostCommentDto>>
    {
        public async Task<Result<PostCommentDto>> Handle(CreatePostCommentCommand request, CancellationToken cancellationToken)
        {
            PostComment comment = await repository.CreatePostCommentAsync(new PostComment
            {
                PostId = request.PostId,
                Content = request.Content,
                UserId = request.UserId
            }, cancellationToken);

            await repository.SaveChangesAsync(cancellationToken);

            return Result<PostCommentDto>.Ok(new PostCommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                UserId = comment.UserId,
                CreatedAt = comment.CreatedAt
            });
        }
    }
}
