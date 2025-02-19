using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a Product in the system with it's name and price informations.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets the description of the product.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets the category of the product.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Gets the image of the product.
        /// </summary>
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// Gets the rating of the product.
        /// </summary>
        public Rating Rating { get; set; } = new Rating(0, 0);
    }
}