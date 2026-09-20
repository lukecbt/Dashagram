namespace Dashagram.Application.Common.Interfaces.Repositories
{
    public interface IBaseRepository
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
