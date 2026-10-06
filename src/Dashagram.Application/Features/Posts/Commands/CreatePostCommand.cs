using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Interfaces.Services;
using Dashagram.Application.Common.Models.Response;
using Dashagram.Application.Common.Models.Storage;
using Dashagram.Application.DTOs.Posts;
using Dashagram.Domain.Models.Entities;
using FluentValidation;
using MediatR;

namespace Dashagram.Application.Features.Posts.Commands
{
    public record CreatePostCommand(CreatePostDto Post, string UserId, IReadOnlyCollection<ImageUpload> Images) : IRequest<Result<PostDto>>;

    public class CreatePostCommandHandler(IPostRepository repository, IStorageService storage) : IRequestHandler<CreatePostCommand, Result<PostDto>>
    {
        /// <summary>
        /// Extensions for image key mapping.
        /// </summary>
        private static readonly Dictionary<string, string> Extensions = new()
        {
            ["image/jpeg"] = ".jpg",
            ["image/png"] = ".png"
        };

        public async Task<Result<PostDto>> Handle (CreatePostCommand request, CancellationToken cancellationToken)
        {
            // Upload images first and then store them alongside the post before saving to database.
            Post post = new()
            {
                Description = request.Post.Description,
                //Images = [.. request.Post.Images.Select(p => new PostImage { Url = p.Url })],
                UserId = request.UserId
            };

            List<PostImage> postImages = [];
            foreach (var image in request.Images)
            {
                // Upload the image to storage and get the key, create the entity and use the Id for the storage key
                PostImage postImage = new()
                {
                    Order = postImages.Count
                };

                string key = $"posts/{post.Id}/{postImage.Id:N}{Extensions[image.ContentType]}";

                await storage.UploadAsync(key, image.Content, image.ContentType, cancellationToken);

                // Set key and add to post images list to add to database alongside the post
                postImage.Key = key;
                postImages.Add(postImage);
            };

            post.Images = postImages;

            post = await repository.CreatePostAsync(post);
            await repository.SaveChangesAsync(cancellationToken);

            return Result<PostDto>.Ok(new PostDto
            {
                Id = post.Id,
                Description = post.Description,
                UserId = post.UserId,
                Images = [.. post.Images?.Select(i => new PostImageDto {
                    Id = i.Id,
                    Key = i.Key,
                    Url = storage.GetPublicUrl(i.Key)
                }) ?? []]
            });
        }
    }

    public class CreatePostValidator : AbstractValidator<CreatePostCommand>
    {
        private static readonly string[] Allowed = ["image/jpeg", "image/png"];

        public CreatePostValidator()
        {
            RuleFor(d => d.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.");
            RuleFor(x => x.Images).Must(i => i.Count <= 10).WithMessage("A post can only have a maximum of 10 images.");
            RuleForEach(x => x.Images).ChildRules(img =>
            {
                img.RuleFor(i => i.Length).InclusiveBetween(1, 5 * 1024 * 1024).WithMessage("Image size must be between 1 and 5 MB.");
                img.RuleFor(i => i.ContentType).Must(Allowed.Contains).WithMessage("Invalid image type.");
            });
        }
    }
}
