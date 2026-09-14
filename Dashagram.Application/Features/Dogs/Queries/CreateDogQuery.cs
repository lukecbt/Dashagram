using Dashagram.Application.Common.Interfaces;
using Dashagram.Application.Dtos.Dogs;
using Dashagram.Domain.Models.Entities;
using MediatR;

namespace Dashagram.Application.Features.Dogs.Queries
{
    public record CreateDogQuery(CreateDogDto Dog) : IRequest<DogDto>;

    public class CreateDogQueryHandler(IDogRepository repository) : IRequestHandler<CreateDogQuery, DogDto>
    {
        public async Task<DogDto> Handle(CreateDogQuery request, CancellationToken cancellationToken)
        {
            Dog? dog = await repository.CreateDogAsync(new Dog
            {
                Name = request.Dog.Name,
                DateOfBirth = request.Dog.DateOfBirth,
                Bio = request.Dog.Bio
            }, cancellationToken);

            return new DogDto
            {
                Name = dog.Name
            };
        }
    }
}
