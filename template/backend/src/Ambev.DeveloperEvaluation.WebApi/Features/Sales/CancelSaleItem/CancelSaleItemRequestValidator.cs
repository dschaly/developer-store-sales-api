using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;

/// <summary>
/// Validator for CancelSaleItemRequest
/// </summary>
public class CancelSaleItemRequestValidator : AbstractValidator<CancelSaleItemRequest>
{
    /// <summary>
    /// Initializes validation rules for CancelSaleItemRequest
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// </remarks>
    public CancelSaleItemRequestValidator()
    {
        RuleFor(x => x.SaleItemId)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
    }
}