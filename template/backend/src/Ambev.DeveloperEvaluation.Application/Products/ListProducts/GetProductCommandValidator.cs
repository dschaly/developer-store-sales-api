using FluentValidation;

/// <summary>
/// Validator for GetProductCommand
/// </summary>
namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class GetProductCommandValidator : AbstractValidator<GetProductCommand>
{
    /// <summary>
    /// Initializes validation rules for GetProductCommand
    /// </summary>
    public GetProductCommandValidator()
    {
        //RuleFor(x => x.Id)
        //    .NotEmpty()
        //    .WithMessage("Product ID is required");
    }
}