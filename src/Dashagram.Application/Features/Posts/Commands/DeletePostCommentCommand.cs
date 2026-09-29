using Dashagram.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Dashagram.Application.Features.Posts.Commands
{
    public record DeletePostCommentCommand(Guid Id) : IRequest<Unit>;

    public class DeletePostCommentCommandHandler(IPostCommentRepository repository) : IRequestHandler<DeletePostCommentCommand, Unit>
    {
        public async Task<Unit> Handle(DeletePostCommentCommand request, CancellationToken cancellationToken)
        {
            await repository.DeletePostCommentAsync(request.Id, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
