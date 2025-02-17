using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;

/// <summary>
/// Validator for CreateBranchRequest that defines validation rules for branch creation.
/// </summary>
public class CreateBranchRequestValidator : AbstractValidator<CreateBranchRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateBranchRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Required, length between 3 and 50 characters
    /// - Address: Required, length between 3 and 50 characters
    /// </remarks>
    public CreateBranchRequestValidator()
    {
        RuleFor(user => user.Name).NotEmpty().Length(3, 50);
        RuleFor(user => user.Address).NotEmpty().Length(3, 50);
    }
}