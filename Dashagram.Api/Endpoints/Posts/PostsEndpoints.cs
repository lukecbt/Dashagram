using Dashagram.Application.Common.Errors;
using Dashagram.Application.Common.Models;
using Dashagram.Application.DTOs.Posts;
using Dashagram.Application.Features.Posts.Commands;
using Dashagram.Application.Features.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace Dashagram.Api.Endpoints.Posts
{
    public static class PostsEndpoints
    {
        public static void RegisterPostsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/posts");
            group.MapGet("/", GetAllPosts);
            group.MapGet("/user/{userId}", GetPostsByUserId);
            group.MapGet("/{id}", GetPostById);
            group.MapPost("/", CreatePost)
                .RequireAuthorization();
            group.MapDelete("/{id}", DeletePost)
                .RequireAuthorization();

            // Likes
            group.MapPost("/{postId}/likes", CreatePostLike)
                .RequireAuthorization();
            group.MapDelete("/likes/{id}", DeletePostLike)
                .RequireAuthorization();
            group.MapPost("/{postId}/likes/count", GetPostLikeCount);
        }

        #region Posts
        static async Task<Ok<Result<IEnumerable<PostDto>>>> GetAllPosts(ISender mediator, int? page, int? size, CancellationToken cancellationToken)
        {
            return TypedResults.Ok(await mediator.Send(new GetAllPostsQuery(page, size), cancellationToken));
        }

        static async Task<Ok<Result<IEnumerable<PostDto>>>> GetPostsByUserId(ISender mediator, string userId, int? page, int? size, CancellationToken cancellationToken)
        {
            return TypedResults.Ok(await mediator.Send(new GetAllPostsByUserIdQuery(userId, page, size), cancellationToken));
        }

        static async Task<Results<Ok<Result<PostDto?>>, BadRequest<string>, NotFound<Result<PostDto?>>>> GetPostById(ISender mediator, Guid? id, CancellationToken cancellationToken)
        {
            if (id is null || id == Guid.Empty)
            {
                return TypedResults.BadRequest("Id is required.");
            }

            var result = await mediator.Send(new GetPostByIdQuery(id.Value), cancellationToken);

            if (result.Code == ErrorCodes.NotFound)
            {
                return TypedResults.NotFound(result);
            }

            return TypedResults.Ok(result);
        }

        static async Task<Results<Created<Result<PostDto>>, BadRequest>> CreatePost(ISender mediator, ClaimsPrincipal user, CreatePostDto post, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new CreatePostCommand(post, user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty), cancellationToken);
            return TypedResults.Created($"/posts", result);
        }

        static async Task<Results<NoContent, BadRequest, NotFound>> DeletePost(ISender mediator, Guid id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeletePostCommand(id), cancellationToken);
            return TypedResults.NoContent();
        }
        #endregion

        #region PostLikes
        static async Task<IResult> CreatePostLike(ISender mediator, ClaimsPrincipal user, Guid postId, CancellationToken cancellationToken)
        {
            Result<Guid> result = await mediator.Send(new CreatePostLikeCommand(postId, user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty), cancellationToken);
            return TypedResults.Created($"/posts/{postId}/likes/{result.Data}", result.Data);
        }

        static async Task<NoContent> DeletePostLike(ISender mediator, Guid id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeletePostLikeCommand(id), cancellationToken);
            return TypedResults.NoContent();
        }

        static async Task<IResult> GetPostLikeCount(ISender mediator, Guid postId, CancellationToken cancellationToken)
        {
            Result<int> result = await mediator.Send(new GetPostLikeCountQuery(postId), cancellationToken);
            return TypedResults.Ok(new { count = result.Data });
        }
        #endregion
    }
}
