﻿using FluentValidation;

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
    /// - Address: Required, length between 3 and 100 characters
    /// </remarks>
    public CreateBranchRequestValidator()
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