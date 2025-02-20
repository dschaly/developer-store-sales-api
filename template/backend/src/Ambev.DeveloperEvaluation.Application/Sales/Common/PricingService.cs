using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Sales.Common;

/// <summary>
/// Service responsible for calculating the pricing of sale items
/// </summary>
public sealed class PricingService : IPricingService
{
    private readonly IProductRepository _productRepository;
    private readonly IDiscountService _discountService;

    /// <summary>
    /// Initializes a new instance of PricingService
    /// </summary>
    /// <param name="productRepository"></param>
    /// <param name="discountService"></param>
    public PricingService(IProductRepository productRepository, IDiscountService discountService)
    {
        _productRepository = productRepository;
        _discountService = discountService;
    }

    /// <summary>
    /// Processes the pricing of a list of sale items
    /// </summary>
    /// <param name="saleItems"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task ProcessSaleItemPricing(SaleItem saleItem, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(saleItem.ProductId, cancellationToken)
            ?? throw new KeyNotFoundException($"Product with ID {saleItem.ProductId} not found");

        var itemDiscount = _discountService.CalculateDiscount(saleItem.Quantity, product.Price);
        saleItem.ApplyDiscount(itemDiscount, product.Price);
        saleItem.CalculateTotalAmount(product.Price);
    }
}