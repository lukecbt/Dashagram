using Dashagram.Application.Common.Interfaces.Services;
using Dashagram.Application.DTOs.Users;
using MediatR;

namespace Dashagram.Application.Features.Users.Commands
{
    public record LoginUserCommand(LoginUserDto Dto) : IRequest<string?>;

    public class LoginUserCommandHandler(IIdentityService identityService, ITokenService tokenService) : IRequestHandler<LoginUserCommand, string?>
    {
        public async Task<string?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await identityService.ValidateUserAsync(request.Dto, cancellationToken);

            if (userId is null)
            {
                return null;
            }

            string token = await tokenService.GenerateTokenAsync(userId, cancellationToken);

            return token;
        }
    }
}
