using FluentValidation;

/// <summary>
/// Validator for GetSaleCommand
/// </summary>
namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListSaleCommandValidator : AbstractValidator<ListSaleCommand>
{
    /// <summary>
    /// Initializes validation rules for GetSaleCommand
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Must not not exceed 50 characters.
    /// - Category: Must not not exceed 50 characters.
    /// </remarks>
    public ListSaleCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(50)
            .WithMessage("he {PropertyName} must not exceed {MaxLength} characters.");
        RuleFor(x => x.Category)
            .MaximumLength(50)
            .WithMessage("he {PropertyName} must not exceed {MaxLength} characters.");
    }
}