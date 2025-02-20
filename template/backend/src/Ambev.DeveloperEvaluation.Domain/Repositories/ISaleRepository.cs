using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Sale entity operations
    /// </summary>
    public interface ISaleRepository : IBaseRepository<Sale>
    {
        /// <summary>
        /// Retrieves a list of sales
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sales list if found, null otherwise</returns>
        Task<IQueryable<Sale>?> Query(CancellationToken cancellationToken = default);
    }
}