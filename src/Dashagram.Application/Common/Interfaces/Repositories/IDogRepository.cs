using Dashagram.Domain.Models.Entities;

namespace Dashagram.Application.Common.Interfaces.Repositories
{
    public interface IDogRepository : IBaseRepository
    {
        Task<Dog?> GetDogByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Dog>> GetAllDogsAsync(CancellationToken cancellationToken = default);
        Task<Dog> CreateDogAsync(Dog dog, CancellationToken cancellationToken = default);
        Task DeleteDogAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
