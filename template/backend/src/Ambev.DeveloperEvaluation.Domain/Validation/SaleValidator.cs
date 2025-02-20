using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for the Sale entity.
/// </summary>
public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.SaleNumber)
            .NotEmpty()
            .WithMessage("Sale number cannot be empty.");

        RuleFor(sale => sale.TotalAmount)
            .GreaterThan(0)
            .WithMessage("Total amount must be greater than zero.");

        RuleFor(sale => sale.IsCancelled)
            .NotNull()
            .WithMessage("Sale cancellation status cannot be null.");

        RuleFor(sale => sale.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID cannot be empty.");

        RuleFor(sale => sale.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID cannot be empty.");

        RuleFor(sale => sale.SaleItems)
            .ForEach(item => item.SetValidator(new SaleItemEntityValidator()));

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
