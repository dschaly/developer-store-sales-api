﻿using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

/// <summary>
/// Validator for CreateProductCommand that defines validation rules for product creation command.
/// </summary>
namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Must not be empty and length between 3 and 50 characters
    /// - Price: Must not be empty and must be greater than zero
    /// - Category: Must not be empty and length between 3 and 50 characters
    /// - Description: Must not be empty and must be null or empty and have a maximum length of 500
    /// - Image: Must not be empty and must be null or empty and have a maximum length of 250
    /// - Rating: Must not be valid (using RatingValidator)
    /// </remarks>
    public CreateProductCommandValidator()
    {
        RuleFor(o => o.Title)
             .NotEmpty()
             .WithMessage("The {PropertyName} is required")
             .Length(3, 50)
             .WithMessage("The {PropertyName} must be between {MinLength} and {MaxLength} characters long.");
        RuleFor(o => o.Price)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required")
            .GreaterThan(0)
            .WithMessage("The {PropertyName} must be greater than zero.");
        RuleFor(o => o.Category)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required")
            .Length(3, 50)
            .WithMessage("The {PropertyName} must be between {MinLength} and {MaxLength} characters long.");
        RuleFor(o => o.Description)
            .MaximumLength(500)
            .WithMessage("The {PropertyName} must not exceed {MaxLength} characters.");
        RuleFor(o => o.Image)
            .MaximumLength(255)
            .WithMessage("The {PropertyName} must not exceed {MaxLength} characters.");
        RuleFor(product => product.Rating).SetValidator(new RatingValidator());
    }
}