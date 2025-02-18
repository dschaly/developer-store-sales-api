using FluentValidation;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for product creation command.
/// </summary>
namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Must not be null or empty, length between 3 and 50 characters
    /// - Email: Must be in valid format (using EmailValidator)
    /// </remarks>
    public CreateSaleCommandValidator()
    {
        //RuleFor(o => o.Name).NotEmpty().Length(3, 50);
        //RuleFor(o => o.Price).NotEmpty().GreaterThan(0);
    }
}