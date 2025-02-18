using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

/// <summary>
/// Validator for CreateCustomerCommand that defines validation rules for customer creation command.
/// </summary>
namespace Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateCustomerCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Must not be null or empty, length between 3 and 50 characters
    /// - Email: Must be in valid format (using EmailValidator)
    /// </remarks>
    public CreateCustomerCommandValidator()
    {
        RuleFor(o => o.Name).NotEmpty().Length(3, 50);
        RuleFor(o => o.Email).SetValidator(new EmailValidator());
    }
}