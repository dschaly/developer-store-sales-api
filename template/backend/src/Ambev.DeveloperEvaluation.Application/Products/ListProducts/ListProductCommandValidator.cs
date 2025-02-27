﻿using FluentValidation;

/// <summary>
/// Validator for GetProductCommand
/// </summary>
namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ListProductCommandValidator : AbstractValidator<ListProductCommand>
{
    /// <summary>
    /// Initializes validation rules for GetProductCommand
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Must not not exceed 50 characters.
    /// - Category: Must not not exceed 50 characters.
    /// </remarks>
    public ListProductCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(50)
            .WithMessage("he {PropertyName} must not exceed {MaxLength} characters.");
        RuleFor(x => x.Category)
            .MaximumLength(50)
            .WithMessage("he {PropertyName} must not exceed {MaxLength} characters.");
    }
}