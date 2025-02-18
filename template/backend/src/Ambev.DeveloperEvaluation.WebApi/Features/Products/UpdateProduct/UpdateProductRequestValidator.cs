using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Validator for CreateProductRequest that defines validation rules for branch updating.
/// </summary>
public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateProductRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Must not be null or empty, length between 3 and 50 characters
    /// - Price: Must not be null or empty, must be greater than 0.
    /// </remarks>
    public UpdateProductRequestValidator()
    {
        RuleFor(o => o.Name).NotEmpty().Length(3, 50);
        RuleFor(o => o.Price).NotEmpty().GreaterThan(0);
    }
}