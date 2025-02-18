using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleCommand that defines validation rules for sale creation command.
/// </summary>
public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Must be null or empty, length between 3 and 50 characters
    /// - Address: Must be null or empty, length between 3 and 100 characters
    /// </remarks>
    public UpdateSaleCommandValidator()
    {
        //RuleFor(o => o.Id).NotEmpty();
        //RuleFor(o => o.Name).NotEmpty().Length(3, 50);
        //RuleFor(o => o.Price).NotEmpty().GreaterThan(0);
    }
}