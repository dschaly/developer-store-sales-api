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
    public DeleteBranchCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Branch ID is required");
    }
}