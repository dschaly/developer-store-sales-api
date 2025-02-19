using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branches.DeleteBranch;

/// <summary>
/// Validator for DeleteBranchCommand
/// </summary>
public class DeleteBranchCommandValidator : AbstractValidator<DeleteBranchCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteBranchCommand
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// </remarks>
    public DeleteBranchCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
    }
}