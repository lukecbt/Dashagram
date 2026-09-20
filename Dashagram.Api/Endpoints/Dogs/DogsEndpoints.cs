using Dashagram.Application.Common.Models;
using Dashagram.Application.Dtos.Dogs;
using Dashagram.Application.Features.Dogs.Commands;
using Dashagram.Application.Features.Dogs.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace Dashagram.Api.Endpoints.Dogs
{
    /// <summary>
    /// Endpoints for managing dogs in the Dashagram API.
    /// </summary>
    public static class DogsEndpoints
    {
        // TODO: Service layer for mapping and other stuff
        public static void RegisterDogsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/dogs");

            group.MapGet("/", GetAllDogs);

            group.MapGet("/{id}", GetDogById);

            group.MapPost("/", CreateDog)
                .RequireAuthorization();

            group.MapDelete("/{id}", DeleteDog);
        }

        static async Task<Ok<IEnumerable<Result<DogDto>>>> GetAllDogs(ISender mediator, CancellationToken cancellationToken)
        {
            return TypedResults.Ok(await mediator.Send(new GetAllDogsQuery(), cancellationToken));
        }

        static async Task<Results<Ok<DogDto>, BadRequest, NotFound>> GetDogById(IMediator mediator, Guid? id)
        {
            if (id is null)
            {
                return TypedResults.BadRequest();
            }

            var result = await mediator.Send(new GetDogByIdQuery(id.Value));

            return TypedResults.Ok(result);
        }

        static async Task<Results<Created<DogDto>, BadRequest>> CreateDog(IMediator mediator, ClaimsPrincipal user, CreateDogDto dog)
        {
            DogDto dogDto = await mediator.Send(new CreateDogCommand(dog, user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty));
            return TypedResults.Created($"/dogs", dogDto);
        }

        static async Task<Results<NoContent, BadRequest, NotFound>> DeleteDog(IMediator mediator, Guid? id)
        {
            if (id is null)
            {
                return TypedResults.BadRequest();
            }
            await mediator.Send(new DeleteDogCommand(id.Value));
            return TypedResults.NoContent();
        }
    }
}