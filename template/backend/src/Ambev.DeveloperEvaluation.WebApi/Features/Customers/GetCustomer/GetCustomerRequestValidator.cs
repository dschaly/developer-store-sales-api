using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.GetCustomer;

/// <summary>
/// Validator for GetCustomerRequest
/// </summary>
public class GetCustomerRequestValidator : AbstractValidator<GetCustomerRequest>
{
    /// <summary>
    /// Initializes validation rules for GetCustomerRequest
    /// </summary>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// </remarks>
    public GetCustomerRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
    }
}
