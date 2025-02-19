using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a Sale Item contained on a Sale in the system with it's quantity, products and prices information.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        /// <summary>
        /// Gets the quantity of the product sold in this sale item.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets the unit price of the product in this sale item.
        /// Must not be null and must be greater than zero.
        /// </summary>
        public decimal UnitPrice { get; set; }

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
        public virtual required Sale Sale { get; set; }

        /// <summary>
        /// Gets the foreign key for the associated product.
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// Gets the product that is sold in this sale item.
        /// </summary>
        public virtual required Product Product { get; set; }
    }
}