using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleRequest that defines validation rules for branch updating.
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Must not be empty and length between 3 and 50 characters
    /// - Price: Must not be empty and must be greater than zero
    /// - Category: Must not be empty and length between 3 and 50 characters
    /// - Description: Must not be empty and must be null or empty and have a maximum length of 500
    /// - Image: Must not be empty and must be null or empty and have a maximum length of 250
    /// - Rating: Must not be valid (using RatingValidator)
    /// </remarks>
    public UpdateSaleRequestValidator()
    {
        RuleFor(o => o.Id)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
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
            .ForEach(item => item.SetValidator(new UpdateSaleItemRequestValidator()));
    }
}

public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
{
    public UpdateSaleItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("The {PropertyName} must be greater than 0.");
    }
}