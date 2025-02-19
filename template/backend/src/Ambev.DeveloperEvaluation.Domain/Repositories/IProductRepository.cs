using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Product entity operations
/// </summary>
public interface IProductRepository : IBaseRepository<Product>
{
    /// <summary>
    /// Retrieves a product by its name
    /// </summary>
    /// <param name="name">The product name to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list if product
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The products list if found, null otherwise</returns>
    Task<IQueryable<Product>?> Query(CancellationToken cancellationToken = default);
}