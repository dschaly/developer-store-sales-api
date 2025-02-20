using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IBaseRepository using Entity Framework Core
/// </summary>
public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly DefaultContext _context;

    public BaseRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new entity in the database
    /// </summary>
    /// <param name="entity">The entity to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created entity</returns>
    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <summary>
    /// Updates a new entity in the database
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated entity</returns>
    public async Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var existingEntity = await GetByIdAsync(entity.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Entity with id {entity.Id} not found");

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return existingEntity;
    }

    /// <summary>
    /// Retrieves an entity by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entity if found, null otherwise</returns>
    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>()
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Deletes an entity from the database
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the entity was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        if (entity is null)
            return false;

        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}