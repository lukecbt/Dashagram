using Dashagram.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Dashagram.Application.Features.Posts.Commands
{
    public record DeletePostCommand(Guid Id) : IRequest<Unit>;
    public class DeleteDogCommandHandler(IPostRepository repository) : IRequestHandler<DeletePostCommand, Unit>
    {
        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            await repository.DeletePostAsync(request.Id, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
