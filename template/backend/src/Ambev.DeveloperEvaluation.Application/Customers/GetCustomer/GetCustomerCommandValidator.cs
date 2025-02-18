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
    public GetCustomerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Customer ID is required");
    }
}