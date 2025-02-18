using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    private readonly DefaultContext _context;

    public CustomerRepository(DefaultContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a customer by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
}