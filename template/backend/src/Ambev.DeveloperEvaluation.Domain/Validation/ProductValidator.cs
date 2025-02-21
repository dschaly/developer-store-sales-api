using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

// <summary>
/// Validator for the Product entity.
/// </summary>
public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Title)
            .NotEmpty()
            .WithMessage("Product title cannot be empty.")
            .MinimumLength(3)
            .WithMessage("Product title must be at least 3 characters long.")
            .MaximumLength(50)
            .WithMessage("Product title cannot be longer than 50 characters.");

        RuleFor(product => product.Price)
            .GreaterThan(0)
            .WithMessage("Product price must be greater than zero.");

        RuleFor(product => product.Description)
            .NotEmpty()
            .WithMessage("Product description cannot be empty.")
            .MaximumLength(500)
            .WithMessage("Product description cannot be longer than 500 characters.");

        RuleFor(product => product.Category)
            .NotEmpty()
            .WithMessage("Product category cannot be empty.")
            .MaximumLength(50)
            .WithMessage("Product category cannot be longer than 50 characters.");

        RuleFor(product => product.Image)
            .NotEmpty()
            .WithMessage("Product image cannot be empty.")
            .MaximumLength(255)
            .WithMessage("Product image URL cannot be longer than 255 characters.");

        RuleFor(product => product.CreatedBy)
            .NotEmpty()
            .WithMessage("CreatedBy cannot be empty.")
            .MaximumLength(100)
            .WithMessage("CreatedBy cannot be longer than 100 characters.");

        RuleFor(product => product.CreatedAt)
            .NotEmpty()
            .WithMessage("CreatedAt cannot be empty.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("CreatedAt cannot be in the future.");

        RuleFor(product => product.UpdatedBy)
            .MaximumLength(100)
            .WithMessage("UpdatedBy cannot be longer than 100 characters.");

        RuleFor(product => product.UpdatedAt)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("UpdatedAt cannot be in the future.");

        RuleFor(product => product.Rating)
            .NotNull()
            .WithMessage("Product rating cannot be null.");

        RuleFor(product => product.Rating.Rate)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Rating rate must be greater than or equal to 0.")
            .LessThanOrEqualTo(5)
            .WithMessage("Rating rate must be less than or equal to 5.");

        RuleFor(product => product.Rating.Count)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Rating count must be greater than or equal to 0.");
    }
}