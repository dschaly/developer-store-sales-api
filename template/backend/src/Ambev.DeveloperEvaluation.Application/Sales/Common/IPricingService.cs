using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.Common;

/// <summary>
/// Service for processing pricing of sale items
/// </summary>
public interface IPricingService
{
    /// <summary>
    /// Processes the pricing of sale items
    /// </summary>
    /// <param name="saleItems"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ProcessSaleItemPricing(SaleItem saleItem, CancellationToken cancellationToken);
}