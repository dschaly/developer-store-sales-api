using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

public class GetByCategoryRequestValidator : AbstractValidator<GetByCategoryRequest>
{
    public GetByCategoryRequestValidator()
    {
        RuleFor(RuleFor => RuleFor.Category)
            .NotEmpty()
            .WithMessage("Category is required");
    }
}