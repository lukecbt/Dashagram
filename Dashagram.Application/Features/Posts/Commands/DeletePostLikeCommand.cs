using Dashagram.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Dashagram.Application.Features.Posts.Commands
{
    public record DeletePostLikeCommand(Guid Id) : IRequest<Unit>;

    public class DeletePostLikeCommandHandler(IPostLikeRepository repository) : IRequestHandler<DeletePostLikeCommand, Unit>
    {
        public async Task<Unit> Handle(DeletePostLikeCommand request, CancellationToken cancellationToken)
        {
            await repository.DeletePostLikeAsync(request.Id, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
