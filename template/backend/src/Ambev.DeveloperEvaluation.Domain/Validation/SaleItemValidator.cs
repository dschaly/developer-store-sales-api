using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("The {PropertyName} must be greater than 0.");
    }
}