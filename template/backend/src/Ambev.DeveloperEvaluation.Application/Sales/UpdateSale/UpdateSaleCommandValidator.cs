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
    /// Validation rules:
    /// - SaleDate: Required, must be greater than the minimum date value.
    /// - CustomerId: Required, must not be empty.
    /// - BranchId: Required, must not be empty.
    /// - SaleItems: Required, must contain at least one item, and each item must meet SaleItemValidatior rules.
    /// </remarks>
    public UpdateSaleCommandValidator()
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