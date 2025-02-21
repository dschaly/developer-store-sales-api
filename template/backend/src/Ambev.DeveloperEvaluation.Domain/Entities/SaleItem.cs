using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a Sale Item contained on a Sale in the system with it's quantity, products and prices information.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
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
    public decimal Discount { get; private set; }

    /// <summary>
    /// Gets the total amount for this sale item (Quantity * UnitPrice - Discount).
    /// Must not be null and must be greater than zero.
    /// </summary>
    public decimal TotalAmount { get; private set; }

    /// <summary>
    /// Gets the foreign key for the associated sale.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets the sale that this sale item belongs to.
    /// </summary>
    public virtual required Sale Sale { get; set; }

    /// <summary>
    /// Gets the product that is sold in this sale item.
    /// </summary>
    public virtual required Product Product { get; set; }

    /// <summary>
    /// Applies a discount to this sale item.
    /// </summary>
    /// <param name="discount"></param>
    public void ApplyDiscount(decimal discount, decimal unitPrice)
    {
        if (discount < 0 || discount > unitPrice * Quantity)
            throw new InvalidOperationException("Discount is invalid.");

        Discount = discount;
    }

    /// <summary>
    /// Calculates the total amout of the sale item.
    /// </summary>
    public void CalculateTotalAmount(decimal unitPrice)
    {
        TotalAmount = Math.Round((unitPrice * Quantity) - Discount, 2);
    }

    /// <summary>
    /// Performs validation of the SaleItem entity using the SaleItemValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">ProductId required</list>
    /// <list type="bullet">Quantity required and value</list>
    /// <list type="bullet">Discount required and value</list>
    /// <list type="bullet">TotalAmount required and value</list>
    /// <list type="bullet">CreatedBy required and length</list>
    /// <list type="bullet">CreatedAt required and value</list>
    /// <list type="bullet">UpdateAt format and value</list>
    /// <list type="bullet">UpdateBy value</list>
    /// 
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleItemEntityValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}