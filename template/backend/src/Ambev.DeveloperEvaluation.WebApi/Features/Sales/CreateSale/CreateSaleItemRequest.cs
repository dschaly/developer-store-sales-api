namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to update a sale item in the system.
/// </summary>
public class CreateSaleItemRequest
{
    /// <summary>
    /// Gets the Id of the product to add to the sale
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets the quantity of the product sold in this sale item.
    /// </summary>
    public int Quantity { get; set; }
}
