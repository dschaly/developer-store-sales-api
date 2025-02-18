using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.UpdateBranch;

/// <summary>
/// Validator for CreateBranchRequest that defines validation rules for branch updating.
/// </summary>
public class UpdateBranchRequestValidator : AbstractValidator<UpdateBranchRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateBranchRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Must be null or empty, length between 3 and 50 characters
    /// - Address: Must be null or empty, length between 3 and 100 characters
    /// </remarks>
    public UpdateBranchRequestValidator()
    {
        RuleFor(o => o.Name).NotEmpty().Length(3, 50);
        RuleFor(o => o.Address).NotEmpty().Length(3, 100);
    }
}