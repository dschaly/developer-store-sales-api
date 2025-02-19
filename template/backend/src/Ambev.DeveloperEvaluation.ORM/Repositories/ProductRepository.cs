using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly DefaultContext _context;

        public ProductRepository(DefaultContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a product by their name
        /// </summary>
        /// <param name="name">The product name to search for</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The product if found, null otherwise</returns>
        public async Task<Product?> GetByTitleAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .FirstOrDefaultAsync(b => b.Title == name, cancellationToken);
        }

        /// <summary>
        /// Retrieves a list of products
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The product list if found, null otherwise</returns>
        public async Task<IQueryable<Product>?> Query(CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(_context.Products.AsNoTracking());
        }
    }
}