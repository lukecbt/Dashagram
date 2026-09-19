namespace Dashagram.Application.Common.Interfaces.Repositories
{
    public interface IApplicationDbContext
    {
        /// <summary>
        /// Expose the SaveChangesAsync method to allow for saving changes to the database context after a transaction.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
