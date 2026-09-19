using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Domain.Models.Entities;
using Dashagram.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Dashagram.Infrastructure.Repositories
{
    public class DogRepository(ApplicationDbContext context) : IDogRepository
    {
        public async Task<Dog?> GetDogByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Dogs.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Dog>> GetAllDogsAsync(CancellationToken cancellationToken = default)
        {
            return await context.Dogs.ToListAsync(cancellationToken);
        }

        public async Task<Dog> CreateDogAsync(Dog dog, CancellationToken cancellationToken = default)
        {
            context.Dogs.Add(dog);
            return dog; //TODO: should we return the dog with the Id populated after saving?
        }

        public async Task DeleteDogAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dog = await context.Dogs.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
            if (dog != null)
            {
                context.Dogs.Remove(dog);
            }
        }
    }
}