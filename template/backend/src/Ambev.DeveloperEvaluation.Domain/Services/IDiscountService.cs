﻿namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface IDiscountService
{
    decimal CalculateDiscount(int quantity, decimal unitPrice);
}