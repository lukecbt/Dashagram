using Dashagram.Application.Common.Interfaces;
using Dashagram.Application.Dtos.Dogs;
using Dashagram.Domain.Models.Entities;
using MediatR;

namespace Dashagram.Application.Features.Dogs.Queries
{
    public record GetAllDogsQuery() : IRequest<IEnumerable<DogDto>>;

    public class GetAllDogsQueryHandler(IDogRepository repository) : IRequestHandler<GetAllDogsQuery, IEnumerable<DogDto>>
    {
        public async Task<IEnumerable<DogDto>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Dog> dogs = await repository.GetAllDogsAsync(cancellationToken);
            return dogs.Select(dog => new DogDto
            {
                Name = dog.Name
            });
        }
    }
}
