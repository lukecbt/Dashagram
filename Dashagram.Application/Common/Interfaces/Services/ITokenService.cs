namespace Dashagram.Application.Common.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(string userId, CancellationToken cancellationToken);
    }
}
