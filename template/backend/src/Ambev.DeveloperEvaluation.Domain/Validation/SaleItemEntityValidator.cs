using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for the SaleItem entity.
/// </summary>
public class SaleItemEntityValidator : AbstractValidator<SaleItem>
{
    public SaleItemEntityValidator()
    {
        RuleFor(saleItem => saleItem.ProductId)
            .NotEmpty()
            .WithMessage("Product ID cannot be empty.");

        RuleFor(saleItem => saleItem.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");

        RuleFor(saleItem => saleItem.Discount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Discount must be greater than or equal to zero.")
            .Must((saleItem, discount) => discount <= saleItem.Product?.Price * saleItem.Quantity)
            .WithMessage("Discount cannot be greater than the total price of the items.");

        RuleFor(saleItem => saleItem.TotalAmount)
            .GreaterThan(0)
            .WithMessage("Total amount must be greater than zero.");

        RuleFor(saleItem => saleItem.SaleId)
            .NotEmpty()
            .WithMessage("Sale ID cannot be empty.");

        RuleFor(saleItem => saleItem.Sale)
            .NotNull()
            .WithMessage("Sale must be associated with the sale item.");

        RuleFor(saleItem => saleItem.Product)
            .NotNull()
            .WithMessage("Product must be associated with the sale item.");

        RuleFor(customer => customer.CreatedBy)
            .NotEmpty()
            .WithMessage("CreatedBy cannot be empty.")
            .MaximumLength(100)
            .WithMessage("CreatedBy cannot exceed 100 characters.");

        RuleFor(customer => customer.CreatedAt)
            .NotEmpty()
            .WithMessage("CreatedAt cannot be empty.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("CreatedAt cannot be in the future.");

        RuleFor(customer => customer.UpdatedBy)
            .MaximumLength(100)
            .WithMessage("UpdatedBy cannot exceed 100 characters.");

        RuleFor(customer => customer.UpdatedAt)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("UpdatedAt cannot be in the future.");
    }
}