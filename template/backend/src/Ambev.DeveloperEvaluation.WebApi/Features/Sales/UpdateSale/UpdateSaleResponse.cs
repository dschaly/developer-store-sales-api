using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// API response model for UpdateSale operation
/// </summary>
public class UpdateSaleResponse : BaseResponse
{
    /// <summary>
    /// The Sale's title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The Sale's price
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets the description of the Sale.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets the category of the Sale.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets the image of the Sale.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets the rating of the Sale.
    /// </summary>
    public Rating Rating { get; set; } = new Rating(0, 0);
}