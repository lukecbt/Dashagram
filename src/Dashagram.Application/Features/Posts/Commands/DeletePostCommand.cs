using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Interfaces.Services;
using Dashagram.Application.Common.Models.Storage;
using Dashagram.Domain.Models.Entities;
using MediatR;

namespace Dashagram.Application.Features.Posts.Commands
{
    public record DeletePostCommand(Guid Id) : IRequest<Unit>;
    public class DeleteDogCommandHandler(IPostRepository repository, IStorageService storage) : IRequestHandler<DeletePostCommand, Unit>
    {
        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            // Delete post and then image
            Post? post = await repository.GetPostByIdAsync(request.Id, cancellationToken);

            if (post is null)
            {
                return Unit.Value;
            }

            await repository.DeletePostAsync(post, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            // Delete images from bucket
            StorageResult storageResult = await storage.DeleteManyAsync(post.Images.Select(i => i.Key), cancellationToken);
            return Unit.Value;
        }
    }
}
