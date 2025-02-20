namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to update a new Sale in the system.
/// </summary>
public class UpdateSaleRequest
{
    /// <summary>
    /// The unique identifier of the Sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the associated customer.
    /// Must not be null or empty.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the branch where the sale was made.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the collection of sale items associated with the sale.
    /// </summary>
    public List<UpdateSaleItemRequest> SaleItems { get; set; } = [];
}