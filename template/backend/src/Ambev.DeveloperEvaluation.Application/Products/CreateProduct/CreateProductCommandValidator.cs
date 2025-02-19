using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

/// <summary>
/// Validator for CreateProductCommand that defines validation rules for product creation command.
/// </summary>
namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Must not be null or empty, length between 3 and 50 characters
    /// - Email: Must be in valid format (using EmailValidator)
    /// - Rating: Must be in valid format (using EmailValidator)
    /// </remarks>
    public CreateProductCommandValidator()
    {
        RuleFor(product => product.Title).NotEmpty().Length(3, 50);
        RuleFor(product => product.Price).NotEmpty().GreaterThan(0);
        RuleFor(product => product.Rating).SetValidator(new RatingValidator());
    }
}