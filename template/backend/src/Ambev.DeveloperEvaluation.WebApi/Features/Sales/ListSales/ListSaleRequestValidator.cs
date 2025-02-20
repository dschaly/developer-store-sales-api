using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

/// <summary>
/// Validator for GetSaleRequest
/// </summary>
public class ListSaleRequestValidator : AbstractValidator<ListSaleRequest>
{
    /// <summary>
    /// Initializes validation rules for GetSaleRequest
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Must not exceed 50 characters
    /// - Category: Must not exceed 50 characters
    /// </remarks>
    public ListSaleRequestValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(50)
            .WithMessage("he {PropertyName} must not exceed {MaxLength} characters.");
        RuleFor(x => x.Category)
            .MaximumLength(50)
            .WithMessage("he {PropertyName} must not exceed {MaxLength} characters.");
    }
}