using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Dtos.Dogs;
using Dashagram.Domain.Models.Entities;
using FluentValidation;
using MediatR;

namespace Dashagram.Application.Features.Dogs.Commands
{
    public record CreateDogCommand : IRequest<DogDto>
    {
        public CreateDogCommand(CreateDogDto dog, string userId)
        {
            Dog = dog;
            UserId = userId;
        }

        public CreateDogDto Dog { get; init; }
        public string UserId { get; init; }
    }

    public class CreateDogCommandHandler(IDogRepository repository, IApplicationDbContext context) : IRequestHandler<CreateDogCommand, DogDto>
    {
        public async Task<DogDto> Handle(CreateDogCommand request, CancellationToken cancellationToken)
        {
            Dog? dog = await repository.CreateDogAsync(new Dog
            {
                Name = request.Dog.Name,
                DateOfBirth = request.Dog.DateOfBirth,
                Bio = request.Dog.Bio,
                OwnerId = request.UserId
            }, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
            // TODO: proper error handling

            return new DogDto
            {
                Name = dog.Name
            };
        }
    }

    public class CreateDogValidator : AbstractValidator<CreateDogCommand>
    {
        public CreateDogValidator()
        {
            RuleFor(d=>d.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.");
            RuleFor(d => d.Dog.Name)
                .NotEmpty().WithMessage("Name is required.");
            RuleFor(d => d.Dog.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Date of birth cannot be in the future.");
            RuleFor(d => d.Dog.Bio)
                .MaximumLength(500).WithMessage("Bio cannot exceed 500 characters.");
        }
    }
}
