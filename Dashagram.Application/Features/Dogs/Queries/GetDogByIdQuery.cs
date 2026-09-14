using Dashagram.Application.Common.Interfaces;
using Dashagram.Application.Dtos.Dogs;
using Dashagram.Domain.Models.Entities;
using MediatR;

namespace Dashagram.Application.Features.Dogs.Queries
{
    public record GetDogByIdQuery(Guid Id) : IRequest<DogDto?>;

    public class GetDogByIdQueryHandler(IDogRepository repository) : IRequestHandler<GetDogByIdQuery, DogDto?>
    {
        public async Task<DogDto?> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {
            Dog? dog = await repository.GetDogByIdAsync(request.Id, cancellationToken);

            if (dog == null)
            {
                return null;
            }

            return new DogDto {
                Name = dog.Name
            };
        }
    }
}
