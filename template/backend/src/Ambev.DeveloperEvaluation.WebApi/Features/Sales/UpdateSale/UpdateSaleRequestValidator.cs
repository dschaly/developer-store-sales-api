using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for sale updating.
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Must not be null or empty, length between 3 and 50 characters
    /// - Price: Must not be null or empty, must be greater than 0.
    /// </remarks>
    public UpdateSaleRequestValidator()
    {
        //RuleFor(o => o.Name).NotEmpty().Length(3, 50);
        //RuleFor(o => o.Price).NotEmpty().GreaterThan(0);
    }
}