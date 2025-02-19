using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.DeleteCustomer;

/// <summary>
/// Validator for DeleteCustomerRequest
/// </summary>
public class DeleteCustomerRequestValidator : AbstractValidator<DeleteCustomerRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteCustomerRequest
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// </remarks
    public DeleteCustomerRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
    }
}