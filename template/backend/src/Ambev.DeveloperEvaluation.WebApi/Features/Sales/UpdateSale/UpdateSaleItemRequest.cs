namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleItemRequest
{
    /// <summary>
    /// Gets the product identifier of the product sold in this sale item.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets the quantity of the product sold in this sale item.
    /// </summary>
    public int Quantity { get; set; }
}