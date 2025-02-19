using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetBranch;

/// <summary>
/// Validator for GetBranchCommand
/// </summary>
public class GetBranchCommandValidator : AbstractValidator<GetBranchCommand>
{
    /// <summary>
    /// Initializes validation rules for GetBranchCommand
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// </remarks>
    public GetBranchCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
    }
}