using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

/// <summary>
/// Validator for GetProductRequest
/// </summary>
public class GetProductRequestValidator : AbstractValidator<GetProductRequest>
{
    /// <summary>
    /// Initializes validation rules for GetProductRequest
    /// </summary>
    public GetProductRequestValidator()
    {
        //RuleFor(x => x.Id)
        //    .NotEmpty()
        //    .WithMessage("Product ID is required");
    }
}
