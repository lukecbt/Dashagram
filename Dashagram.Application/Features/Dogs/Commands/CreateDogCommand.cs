using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Dtos.Dogs;
using Dashagram.Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashagram.Application.Features.Dogs.Commands
{
    public record CreateDogCommand(CreateDogDto Dog) : IRequest<DogDto>;

    public class CreateDogCommandHandler(IDogRepository repository) : IRequestHandler<CreateDogCommand, DogDto>
    {
        public async Task<DogDto> Handle(CreateDogCommand request, CancellationToken cancellationToken)
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
