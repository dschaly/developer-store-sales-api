using FluentValidation;

/// <summary>
/// Validator for GetSaleCommand
/// </summary>
namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>
{
    /// <summary>
    /// Initializes validation rules for GetSaleCommand
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Must not not exceed 50 characters.
    /// - Category: Must not not exceed 50 characters.
    /// </remarks>
    public GetSaleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
    }
}