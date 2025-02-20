using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleCommand that defines validation rules for sale creation command.
/// </summary>
public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// - Title: Must not be empty and length between 3 and 50 characters
    /// - Price: Must not be empty and must be greater than zero
    /// - Category: Must not be empty and length between 3 and 50 characters
    /// - Description: Must not be empty and must be null or empty and have a maximum length of 500
    /// - Image: Must not be empty and must be null or empty and have a maximum length of 250
    /// - Rating: Must not be valid (using RatingValidator)
    /// </remarks>
    public UpdateSaleCommandValidator()
    {
        RuleFor(o => o.Id)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
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
            .NotEmpty()
            .WithMessage("The {PropertyName} is required")
            .MaximumLength(500)
            .WithMessage("The {PropertyName} must not exceed {MaxLength} characters.");
        RuleFor(o => o.Image)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required")
            .MaximumLength(255)
            .WithMessage("The {PropertyName} must not exceed {MaxLength} characters.");
        RuleFor(sale => sale.Rating).SetValidator(new RatingValidator());
    }
}