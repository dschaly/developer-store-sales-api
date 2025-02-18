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
    /// Validation rules include:
    /// - Name: Required, length between 3 and 50 characters
    /// - Email: Must be valid format (using EmailValidator)
    /// </remarks>
    public CreateSaleRequestValidator()
    {
        //RuleFor(user => user.Name).NotEmpty().Length(3, 50);
        //RuleFor(user => user.Price).NotEmpty().GreaterThan(0);
    }
}