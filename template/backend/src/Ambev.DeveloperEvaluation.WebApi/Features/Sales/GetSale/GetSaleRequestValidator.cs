using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Validator for GetSaleRequest
/// </summary>
public class GetSaleRequestValidator : AbstractValidator<GetSaleRequest>
{
    /// <summary>
    /// Initializes validation rules for GetSaleRequest
    /// </summary>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// </remarks>
    public GetSaleRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
    }
}
