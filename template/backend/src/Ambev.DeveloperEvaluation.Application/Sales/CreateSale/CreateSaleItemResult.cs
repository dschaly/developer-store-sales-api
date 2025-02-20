using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleItemResult
{
    /// <summary>
    /// Gets the unique identifier.
    /// </summary>
    public Guid Id { get; set; }

    // <summary>
    /// Gets the foreign key for the associated product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets the quantity of the product sold in this sale item.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the discount applied to this sale item, if any.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets the total amount for this sale item (Quantity * UnitPrice - Discount).
    /// Must not be null and must be greater than zero.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets the foreign key for the associated sale.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets the sale that this sale item belongs to.
    /// </summary>
    public virtual required CreateSaleResult Sale { get; set; }

    /// <summary>
    /// Gets the product that is sold in this sale item.
    /// </summary>
    public virtual required Product Product { get; set; }
}
