using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

/// <summary>
/// Validator for CreateBranchCommand that defines validation rules for branch creation command.
/// </summary>
public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateBranchCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Must not be null or empty
    /// - Address: Must not be null or empty
    /// </remarks>
    public CreateBranchCommandValidator()
    {
        RuleFor(o => o.Name).NotEmpty()
            .WithMessage("The {PropertyName} is required.")
            .Length(3, 50)
            .WithMessage("The {PropertyName} must be between {MinLength} and {MaxLength} characters long.");
        RuleFor(o => o.Address).NotEmpty()
            .WithMessage("The {PropertyName} is required.")
            .Length(3, 100)
            .WithMessage("The {PropertyName} must be between {MinLength} and {MaxLength} characters long.");
    }
}