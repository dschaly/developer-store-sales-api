using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;

/// <summary>
/// Validator for CreateCustomerRequest that defines validation rules for branch creation.
/// </summary>
public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateCustomerRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Required, length between 3 and 50 characters
    /// - Email: Must be valid format (using EmailValidator)
    /// </remarks>
    public CreateCustomerRequestValidator()
    {
        RuleFor(user => user.Name).NotEmpty().Length(3, 50);
        RuleFor(user => user.Email).SetValidator(new EmailValidator());
    }
}