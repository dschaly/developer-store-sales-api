using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleItemRepository : BaseRepository<SaleItem>, ISaleItemRepository
    {
        private readonly DefaultContext _context;
        public SaleItemRepository(DefaultContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a Sale Item by it's unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the sale item</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale item if found, null otherwise</returns>
        public override async Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.SaleItems
                .Include(x => x.Sale)
                .ThenInclude(y => y.SaleItems)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }
    }
}