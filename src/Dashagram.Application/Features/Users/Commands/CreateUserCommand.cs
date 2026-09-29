using Dashagram.Application.Common.Interfaces.Services;
using Dashagram.Application.DTOs.Users;
using MediatR;

namespace Dashagram.Application.Features.Users.Commands
{
    public record CreateUserCommand(CreateUserDto Dto) : IRequest<string>;

    public class CreateUserCommandHandler(IIdentityService service) : IRequestHandler<CreateUserCommand, string>
    {
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await service.CreateUserAsync(request.Dto, cancellationToken);
        }
    }
}
