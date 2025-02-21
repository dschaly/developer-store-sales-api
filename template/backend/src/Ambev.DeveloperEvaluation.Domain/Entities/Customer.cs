using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

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

        /// <summary>
        /// Performs validation of the Customer entity using the CustomerValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed
        /// - Errors: Collection of validation errors if any rules failed
        /// </returns>
        /// <remarks>
        /// <listheader>The validation includes checking:</listheader>
        /// <list type="bullet">Name required and length</list>
        /// <list type="bullet">Email format</list>
        /// <list type="bullet">CreatedBy required and length</list>
        /// <list type="bullet">CreatedAt required and value</list>
        /// <list type="bullet">UpdateAt format and value</list>
        /// <list type="bullet">UpdateBy value</list>
        /// 
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new CustomerValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}