using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductRequest that defines validation rules for product creation.
/// </summary>
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Required, length between 3 and 50 characters
    /// - Email: Must be valid format (using EmailValidator)
    /// </remarks>
    public CreateProductRequestValidator()
    {
        RuleFor(user => user.Name).NotEmpty().Length(3, 50);
        RuleFor(user => user.Price).NotEmpty().GreaterThan(0);
    }
}