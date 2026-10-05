using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Dashagram.Api.Endpoints.Antiforgery
{
    public static class AntiforgeryEndpoints
    {
        public static void RegisterAntiforgeryEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/antiforgery");
            group.MapGet("/token", GetAntiforgeryToken)
                .RequireAuthorization();
        }

        static async Task<Results<Ok<string>, BadRequest<string>>> GetAntiforgeryToken(HttpContext httpContext, IAntiforgery antiforgery)
        {
            try
            {
                var tokens = antiforgery.GetAndStoreTokens(httpContext);
                return TypedResults.Ok(tokens.RequestToken);
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }
        }
    }
}
