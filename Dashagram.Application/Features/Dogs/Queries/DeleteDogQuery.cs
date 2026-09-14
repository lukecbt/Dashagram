using Dashagram.Application.Common.Interfaces;
using MediatR;

namespace Dashagram.Application.Features.Dogs.Queries
{
    public record DeleteDogQuery(Guid Id) : IRequest<Unit>;
    public class DeleteDogQueryHandler(IDogRepository repository) : IRequestHandler<DeleteDogQuery, Unit>
    {
        public async Task<Unit> Handle(DeleteDogQuery request, CancellationToken cancellationToken)
        {
            await repository.DeleteDogAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
