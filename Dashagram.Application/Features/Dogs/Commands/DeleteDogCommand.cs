using Dashagram.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Dashagram.Application.Features.Dogs.Commands
{
    public record DeleteDogCommand(Guid Id) : IRequest<Unit>;
    public class DeleteDogCommandHandler(IDogRepository repository, IApplicationDbContext context) : IRequestHandler<DeleteDogCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteDogCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteDogAsync(request.Id, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
