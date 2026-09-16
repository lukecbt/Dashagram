using Dashagram.Application.DTOs.Users;

namespace Dashagram.Application.Common.Interfaces.Services
{
    public interface IIdentityService
    {
        /// <summary>
        /// Creates a new user and returns the user id.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> CreateUserAsync(CreateUserDto dto, CancellationToken cancellationToken);
        /// <summary>
        /// Validates the user credentials and returns a user id if valid, otherwise returns null.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string?> ValidateUserAsync(LoginUserDto dto, CancellationToken cancellationToken);
    }
}
