using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.DeleteBranch;

/// <summary>
/// Validator for DeleteBranchRequest
/// </summary>
public class DeleteBranchRequestValidator : AbstractValidator<DeleteBranchRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteBranchRequest
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// </remarks>
    public DeleteBranchRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
    }
}