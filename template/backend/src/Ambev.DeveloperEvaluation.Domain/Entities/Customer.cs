using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a Customer in the system with it's name and e-mail informations.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class Customer : BaseEntity
    {
        /// <summary>
        /// Gets the name of the customer.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets the email address of the customer.
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}