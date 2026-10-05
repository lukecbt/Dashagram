using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Models.Response;
using MediatR;

namespace Dashagram.Application.Features.Posts.Queries
{
    public record GetPostLikeCountQuery(Guid postId) : IRequest<Result<int>>;

    public class GetPostLikeCountQueryHandler(IPostLikeRepository repository) : IRequestHandler<GetPostLikeCountQuery, Result<int>>
    {
        public async Task<Result<int>> Handle(GetPostLikeCountQuery request, CancellationToken cancellationToken)
        {
            int count = await repository.CountAsync(request.postId, cancellationToken);
            return Result<int>.Ok(count);
        }
    }
}
