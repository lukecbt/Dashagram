using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Models;
using Dashagram.Application.Dtos.Dogs;
using Dashagram.Domain.Models.Entities;
using MediatR;

namespace Dashagram.Application.Features.Dogs.Queries
{
    public record GetAllDogsQuery() : IRequest<IEnumerable<Result<DogDto>>>;

    public class GetAllDogsQueryHandler(IDogRepository repository) : IRequestHandler<GetAllDogsQuery, IEnumerable<Result<DogDto>>>
    {
        public async Task<IEnumerable<Result<DogDto>>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Dog> dogs = await repository.GetAllDogsAsync(cancellationToken);
            return dogs.Select(dog => Result<DogDto>.Ok(new DogDto
            {
                Name = dog.Name,
                DateOfBirth = dog.DateOfBirth,
                Breed = dog.Breed.Name,
                Bio = dog.Bio
            }));
        }
    }
}
