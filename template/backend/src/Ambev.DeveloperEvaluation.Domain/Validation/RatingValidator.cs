using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class RatingValidator : AbstractValidator<Rating?>
{
    public RatingValidator()
    {
        RuleFor(rating => rating)
            .NotNull()
            .When(r => r is not null)
            .Must(r => r!.Rate >= 0 && r.Rate <= 5 && r.Count >= 0)
            .When(r => r is not null)
            .WithMessage("The rating provided is invalid");
    }
}