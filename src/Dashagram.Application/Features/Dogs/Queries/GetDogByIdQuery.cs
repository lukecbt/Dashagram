using Dashagram.Application.Common.Errors;
using Dashagram.Application.Common.Helpers;
using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Models.Response;
using Dashagram.Application.Dtos.Dogs;
using Dashagram.Domain.Models.Entities;
using MediatR;

namespace Dashagram.Application.Features.Dogs.Queries
{
    public record GetDogByIdQuery(Guid Id) : IRequest<Result<DogDto?>>;

    public class GetDogByIdQueryHandler(IDogRepository repository) : IRequestHandler<GetDogByIdQuery, Result<DogDto?>>
    {
        public async Task<Result<DogDto?>> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {
            Dog? dog = await repository.GetDogByIdAsync(request.Id, cancellationToken);

            if (dog == null)
            {
                return Result<DogDto?>.Fail(ErrorCodes.NotFound, [new Error
                {
                    Details = ErrorHelper.NotFound("Dog", request.Id),
                    Message = ErrorHelper.NotFound("Dog")
                }]);
            }

            return Result<DogDto?>.Ok(new DogDto
            {
                Name = dog.Name
            });
        }
    }
}
