using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Customers.UpdateCustomer;

/// <summary>
/// Validator for UpdateCustomerCommand that defines validation rules for customer creation command.
/// </summary>
public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateCustomerCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Must be null or empty, length between 3 and 50 characters
    /// - Address: Must be null or empty, length between 3 and 100 characters
    /// </remarks>
    public UpdateCustomerCommandValidator()
    {
        RuleFor(o => o.Id).NotEmpty();
        RuleFor(o => o.Name).NotEmpty().Length(3, 50);
        RuleFor(o => o.Email).SetValidator(new EmailValidator());
    }
}