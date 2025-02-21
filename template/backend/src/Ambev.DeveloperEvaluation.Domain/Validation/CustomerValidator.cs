using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for the Customer entity.
/// </summary>
public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty()
            .WithMessage("Customer name cannot be empty.")
            .MinimumLength(3)
            .WithMessage("Customer name must be at least 3 characters long.")
            .MaximumLength(100)
            .WithMessage("Customer name cannot be longer than 100 characters.");

        RuleFor(customer => customer.Email)
            .NotEmpty()
            .WithMessage("Customer email cannot be empty.")
            .EmailAddress()
            .WithMessage("Customer email must be a valid email address.");

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