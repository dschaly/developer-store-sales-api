using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

public class ListByCategoryRequestValidator : AbstractValidator<ListByCategoryRequest>
{
    /// <summary>
    /// Initializes validation rules for ListByCategoryRequest
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Category: Must not be empty and must not exceed 50 characters
    /// </remarks>
    public ListByCategoryRequestValidator()
    {
        RuleFor(RuleFor => RuleFor.Category)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.")
            .MaximumLength(50)
            .WithMessage("he {PropertyName} must not exceed {MaxLength} characters.");
    }
}