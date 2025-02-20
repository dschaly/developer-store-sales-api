using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Represents the response returned after successfully updating a new Sale.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the recently updated user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public sealed class UpdateSaleResult : BaseResult
{
    /// <summary>
    /// Gets the sale number.
    /// This is a unique identifier for the sale transaction.
    /// </summary>
    public string SaleNumber { get; private set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total amount of the sale after applying any discounts.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets a value indicating whether the sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the associated customer.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the branch where the sale was made.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the collection of sale items associated with the sale.
    /// </summary>
    public List<UpdateSaleItemResult> SaleItems { get; set; } = [];
}