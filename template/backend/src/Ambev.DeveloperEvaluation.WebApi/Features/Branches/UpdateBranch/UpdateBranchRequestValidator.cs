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
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.")
            .Length(3, 50)
            .WithMessage("The {PropertyName} must be between {MinLength} and {MaxLength} characters long.");
        RuleFor(user => user.Address)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.")
            .Length(3, 100)
            .WithMessage("The {PropertyName} must be between {MinLength} and {MaxLength} characters long.");
    }
}