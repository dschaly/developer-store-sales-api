using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules:
    /// - SaleDate: Required, must be greater than the minimum date value.
    /// - CustomerId: Required, must not be empty.
    /// - BranchId: Required, must not be empty.
    /// - SaleItems: Required, must contain at least one item, and each item must meet SaleItemValidatior rules.
    /// </remarks>
    public CreateSaleRequestValidator()
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
            .ForEach(item => item.SetValidator(new CreateSaleItemRequestValidator()));
    }
}

public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{
    public CreateSaleItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("The {PropertyName} must be greater than 0.");
    }
}