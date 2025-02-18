using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Customer entity operations
/// </summary>
public interface ICustomerRepository : IBaseRepository<Customer>
{
    /// <summary>
    /// Retrieves a customer by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The customer if found, null otherwise</returns>
    Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}