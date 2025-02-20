using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Validator for CancelSaleCommand
/// </summary>
public class CancelSaleCommandValidator : AbstractValidator<CancelSaleCommand>
{
    /// <summary>
    /// Initializes validation rules for CancelSaleCommand
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// </remarks>
    public CancelSaleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
    }
}