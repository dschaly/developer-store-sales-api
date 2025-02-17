using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Branch entity operations
    /// </summary>
    public interface IBranchRepository : IBaseRepository<Branch>
    {
        /// <summary>
        /// Retrieves a branch by its name
        /// </summary>
        /// <param name="name">The branch name to search for</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The branch if found, null otherwise</returns>
        Task<Branch?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}