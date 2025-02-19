using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Represents the response returned after successfully creating a new Product.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public sealed class CreateProductResult : BaseResult
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