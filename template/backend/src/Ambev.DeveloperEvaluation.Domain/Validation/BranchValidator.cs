using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for the Branch entity.
/// </summary>
public class BranchValidator : AbstractValidator<Branch>
{
    public BranchValidator()
    {
        RuleFor(branch => branch.Name)
            .NotEmpty()
            .WithMessage("Branch name cannot be empty.")
            .MinimumLength(3)
            .WithMessage("Branch name must be at least 3 characters long.")
            .MaximumLength(100)
            .WithMessage("Branch name cannot be longer than 100 characters.");

        RuleFor(branch => branch.Address)
            .NotEmpty()
            .WithMessage("Branch address cannot be empty.")
            .MaximumLength(200)
            .WithMessage("Branch address cannot be longer than 200 characters.");

        RuleFor(branch => branch.CreatedBy)
            .MaximumLength(100)
            .WithMessage("CreatedBy cannot be longer than 100 characters.");

        RuleFor(branch => branch.CreatedAt)
            .NotEmpty()
            .WithMessage("CreatedAt cannot be empty.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("CreatedAt cannot be in the future.");


        RuleFor(branch => branch.UpdatedBy)
            .MaximumLength(100)
            .WithMessage("UpdatedBy cannot be longer than 100 characters.");

        RuleFor(branch => branch.UpdatedAt)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("UpdatedAt cannot be in the future.");
    }
}