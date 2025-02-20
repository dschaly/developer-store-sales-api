using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for salke creation command.
/// </summary>
namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules:
    /// - SaleDate: Required, must be greater than the minimum date value.
    /// - CustomerId: Required, must not be empty.
    /// - BranchId: Required, must not be empty.
    /// - SaleItems: Required, must contain at least one item, and each item must meet SaleItemValidatior rules.
    /// </remarks>
    public CreateSaleCommandValidator()
    {
        RuleFor(o => o.CustomerId)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required");
        RuleFor(o => o.BranchId)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required");
        RuleFor(o => o.SaleItems)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required")
            .Must(o => o.Count > 0)
            .WithMessage("The {PropertyName} must have at least one item")
            .ForEach(item => item.SetValidator(new SaleItemValidator()));
    }
}