using FluentValidation;

/// <summary>
/// Validator for GetCustomerCommand
/// </summary>
namespace Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

public class GetCustomerCommandValidator : AbstractValidator<GetCustomerCommand>
{
    /// <summary>
    /// Initializes validation rules for GetCustomerCommand
    /// </summary>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// </remarks>
    public GetCustomerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
    }
}