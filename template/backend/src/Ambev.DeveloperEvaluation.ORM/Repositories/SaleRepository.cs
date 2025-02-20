using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        private readonly DefaultContext _context;
        public SaleRepository(DefaultContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of sales
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale list if found, null otherwise</returns>
        public async Task<IQueryable<Sale>?> Query(CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(_context.Sales.AsNoTracking());
        }
    }
}