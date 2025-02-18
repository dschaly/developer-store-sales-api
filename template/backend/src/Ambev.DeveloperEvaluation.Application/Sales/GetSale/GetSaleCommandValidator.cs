﻿using FluentValidation;

/// <summary>
/// Validator for GetSaleCommand
/// </summary>
namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>
{
    /// <summary>
    /// Initializes validation rules for GetSaleCommand
    /// </summary>
    public GetSaleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}