using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Validator for UpdateProductCommand that defines validation rules for product creation command.
/// </summary>
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Must be null or empty, length between 3 and 50 characters
    /// - Address: Must be null or empty, length between 3 and 100 characters
    /// </remarks>
    public UpdateProductCommandValidator()
    {
        RuleFor(o => o.Id).NotEmpty();
        RuleFor(o => o.Name).NotEmpty().Length(3, 50);
        RuleFor(o => o.Price).NotEmpty().GreaterThan(0);
    }
}