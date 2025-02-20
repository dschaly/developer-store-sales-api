using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Represents the response returned after successfully updating a new Sale.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the recently updated user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public sealed class UpdateSaleResult : BaseResult
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
    public Rating? Rating { get; set; }
}