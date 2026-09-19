using Dashagram.Application.DTOs.Users;
using Dashagram.Application.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Dashagram.Api.Endpoints.Auth
{
    public static class AuthEndpoints
    {
        public static void RegisterAuthEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/auth");

            group.MapPost("/register", CreateUser);

            group.MapPost("/login", LoginUser);
        }

        static async Task<Results<Created<string>, BadRequest>> CreateUser(IMediator mediator, CreateUserDto user)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                return TypedResults.BadRequest();
            }
            string userId = await mediator.Send(new CreateUserCommand(user));
            return TypedResults.Created($"/auth/register", userId);
        }

        static async Task<Results<Ok<string>, BadRequest>> LoginUser(IMediator mediator, LoginUserDto user)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                return TypedResults.BadRequest();
            }
            string? token = await mediator.Send(new LoginUserCommand(user));

            if (token is null)
            {
                return TypedResults.BadRequest();
            }

            return TypedResults.Ok(token);
        }
    }
}