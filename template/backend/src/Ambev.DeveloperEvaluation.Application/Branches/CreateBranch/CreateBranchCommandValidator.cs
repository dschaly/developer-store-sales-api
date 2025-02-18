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
        RuleFor(o => o.Name).NotEmpty().Length(3, 50);
        RuleFor(o => o.Address).NotEmpty().Length(3, 100);
    }
}