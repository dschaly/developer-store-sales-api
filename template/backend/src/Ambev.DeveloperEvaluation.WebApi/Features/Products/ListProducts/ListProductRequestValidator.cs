using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

/// <summary>
/// Validator for GetProductRequest
/// </summary>
public class ListProductRequestValidator : AbstractValidator<ListProductRequest>
{
    /// <summary>
    /// Initializes validation rules for GetProductRequest
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Must not exceed 50 characters
    /// - Category: Must not exceed 50 characters
    /// </remarks>
    public ListProductRequestValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(50)
            .WithMessage("he {PropertyName} must not exceed {MaxLength} characters.");
        RuleFor(x => x.Category)
            .MaximumLength(50)
            .WithMessage("he {PropertyName} must not exceed {MaxLength} characters.");
    }
}