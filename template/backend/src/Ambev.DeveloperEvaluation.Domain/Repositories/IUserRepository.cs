using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for User entity operations
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Retrieves a list if users
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The users list if found, null otherwise</returns>
    Task<IQueryable<User>?> Query(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
