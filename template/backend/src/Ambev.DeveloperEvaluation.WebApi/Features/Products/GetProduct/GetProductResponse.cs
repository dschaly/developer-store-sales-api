using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// API response model for GetProduct operation
/// </summary>
public class GetProductResponse
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public List<ProductResponse>? Data { get; set; }
}

public class ProductResponse : BaseResult
{
    /// <summary>
    /// The Product's title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The Product's price
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets the description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets the category of the product.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets the image of the product.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets the rating of the product.
    /// </summary>
    public Rating? Rating { get; set; }
}