using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.UpdateCustomer;

/// <summary>
/// Validator for CreateCustomerRequest that defines validation rules for branch updating.
/// </summary>
public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateCustomerRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Must not be null or empty, length between 3 and 50 characters
    /// - Email: Must not be valid format (using EmailValidator)
    /// </remarks>
    public UpdateCustomerRequestValidator()
    {
        RuleFor(o => o.Name).NotEmpty().Length(3, 50);
        RuleFor(o => o.Email).NotEmpty().SetValidator(new EmailValidator());
    }
}