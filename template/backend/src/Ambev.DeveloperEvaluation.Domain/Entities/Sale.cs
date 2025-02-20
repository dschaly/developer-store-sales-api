using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a Sale in the system with it's items, customer and branch information.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// Gets the sale number.
        /// This is a unique identifier for the sale transaction.
        /// Must not be null or empty and must be greater than zero.
        /// </summary>
        public string SaleNumber { get; private set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total amount of the sale after applying any discounts.
        /// Must not be null and must be greater than zero.
        /// </summary>
        public decimal TotalAmount { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the sale has been cancelled.
        /// Must not be null.
        /// </summary>
        public bool IsCancelled { get; private set; } = false;

        /// <summary>
        /// Gets or sets the foreign key for the associated customer.
        /// Must not be null or empty.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer who made the sale.
        /// </summary>
        public virtual required Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the branch where the sale was made.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Gets or sets the branch where the sale took place.
        /// </summary>
        public virtual required Branch Branch { get; set; }

        /// <summary>
        /// Gets or sets the collection of sale items associated with the sale.
        /// </summary>
        public virtual List<SaleItem> SaleItems { get; set; } = [];

        /// <summary>
        /// Generates and sets a new sale number.
        /// </summary>
        public void GenerateSaleNumber()
        {
            SaleNumber = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Generates and sets a new sale number.
        /// </summary>
        public void CalculateTotalAmout()
        {
            TotalAmount = Math.Round(SaleItems.Sum(item => item.TotalAmount), 2);
        }

        /// <summary>
        /// Cancels the sale.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void CancelSale()
        {
            if (IsCancelled)
                throw new InvalidOperationException("Sale has already been cancelled.");

            IsCancelled = true;
        }
    }
}