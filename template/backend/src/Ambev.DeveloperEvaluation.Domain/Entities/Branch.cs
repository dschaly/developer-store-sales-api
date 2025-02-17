using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a Branch in the system with it's name and address informations.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class Branch : BaseEntity
    {
        /// <summary>
        /// Gets the name of the branch.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets the address of the branch.
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}