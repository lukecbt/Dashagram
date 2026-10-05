using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Models.Response;
using Dashagram.Application.Dtos.Dogs;
using Dashagram.Domain.Models.Entities;
using MediatR;

namespace Dashagram.Application.Features.Dogs.Queries
{
    public record GetAllDogsQuery() : IRequest<Result<IEnumerable<DogDto>>>;

    public class GetAllDogsQueryHandler(IDogRepository repository) : IRequestHandler<GetAllDogsQuery, Result<IEnumerable<DogDto>>>
    {
        public async Task<Result<IEnumerable<DogDto>>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Dog> dogs = await repository.GetAllDogsAsync(cancellationToken);
            
            return Result<IEnumerable<DogDto>>.Ok(dogs.Select(dog => new DogDto
            {
                Name = dog.Name,
                DateOfBirth = dog.DateOfBirth,
                Breed = dog.Breed.Name,
                Bio = dog.Bio
            }));
        }
    }
}
