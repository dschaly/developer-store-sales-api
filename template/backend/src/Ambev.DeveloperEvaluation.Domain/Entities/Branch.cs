using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

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

    /// <summary>
    /// Performs validation of the Branch entity using the BranchValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Name required and length</list>
    /// <list type="bullet">Address required and length</list>
    /// <list type="bullet">CreatedBy required and length</list>
    /// <list type="bullet">CreatedAt required and value</list>
    /// <list type="bullet">UpdateAt format and value</list>
    /// <list type="bullet">UpdateBy value</list>
    /// 
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new BranchValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}