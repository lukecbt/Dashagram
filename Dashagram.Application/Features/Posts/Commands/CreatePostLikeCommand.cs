using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Models;
using Dashagram.Domain.Models.Entities;
using MediatR;

namespace Dashagram.Application.Features.Posts.Commands
{
    public record CreatePostLikeCommand(Guid PostId, string UserId) : IRequest<Result<Guid>>;

    public class CreatePostLikeCommandHandler(IPostLikeRepository repository) : IRequestHandler<CreatePostLikeCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle (CreatePostLikeCommand request, CancellationToken cancellationToken)
        {
            PostLike postLike = await repository.CreatePostLikeAsync(new PostLike
            {
                PostId = request.PostId,
                UserId = request.UserId
            }, cancellationToken);

            await repository.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(postLike.Id);
        }
    }
}
