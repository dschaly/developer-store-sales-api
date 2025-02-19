using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetBranch;

/// <summary>
/// Validator for GetBranchRequest
/// </summary>
public class GetBranchRequestValidator : AbstractValidator<GetBranchRequest>
{
    /// <summary>
    /// Initializes validation rules for GetBranchRequest
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// </remarks>
    public GetBranchRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
    }
}
