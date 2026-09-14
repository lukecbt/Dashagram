using Dashagram.Application.Dtos.Dogs;
using Dashagram.Application.Features.Dogs.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

            group.MapPost("/", CreateDog);

            group.MapDelete("/{id}", DeleteDog);
        }

        static async Task<Ok<IEnumerable<DogDto>>> GetAllDogs([FromServices] IMediator mediator)
        {
            return TypedResults.Ok(await mediator.Send(new GetAllDogsQuery()));
        }

        static async Task<Results<Ok<DogDto>, BadRequest, NotFound>> GetDogById([FromServices] IMediator mediator, Guid? id)
        {
            if (id is null)
            {
                return TypedResults.BadRequest();
            }

            var result = await mediator.Send(new GetDogByIdQuery(id.Value));

            return TypedResults.Ok(result);
        }

        static async Task<Results<Created<DogDto>, BadRequest>> CreateDog([FromServices] IMediator mediator, CreateDogDto dog)
        {
            if (string.IsNullOrWhiteSpace(dog.Name))
            {
                return TypedResults.BadRequest();
            }
            DogDto dogDto = await mediator.Send(new CreateDogQuery(dog));
            return TypedResults.Created($"/dogs", dogDto);
        }

        static async Task<Results<NoContent, BadRequest, NotFound>> DeleteDog([FromServices] IMediator mediator, Guid? id)
        {
            if (id is null)
            {
                return TypedResults.BadRequest();
            }
            await mediator.Send(new DeleteDogQuery(id.Value));
            return TypedResults.NoContent();
        }
    }
}