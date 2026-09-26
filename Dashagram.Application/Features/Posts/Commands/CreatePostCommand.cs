using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Models;
using Dashagram.Application.DTOs.Posts;
using Dashagram.Domain.Models.Entities;
using FluentValidation;
using MediatR;

namespace Dashagram.Application.Features.Posts.Commands
{
    public record CreatePostCommand(CreatePostDto Post, string UserId) : IRequest<Result<PostDto>>;

    public class CreatePostCommandHandler(IPostRepository repository) : IRequestHandler<CreatePostCommand, Result<PostDto>>
    {
        public async Task<Result<PostDto>> Handle (CreatePostCommand request, CancellationToken cancellationToken)
        {
            Post post = await repository.CreatePostAsync(new Post
            {
                Title = request.Post.Title,
                Description = request.Post.Description,
                Images = [.. request.Post.Images.Select(p => new PostImage { Url = p.Url })],
                UserId = request.UserId
            });

            await repository.SaveChangesAsync(cancellationToken);

            return Result<PostDto>.Ok(new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                UserId = post.UserId
            });
        }
    }

    public class CreatePostValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostValidator()
        {
            RuleFor(d => d.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.");
            RuleFor(d => d.Post.Images)
                .NotEmpty()
                .WithMessage("Post must have at least one image.");
        }
    }
}
