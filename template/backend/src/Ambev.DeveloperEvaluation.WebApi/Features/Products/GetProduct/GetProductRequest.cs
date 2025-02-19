namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Request model for getting a product by ID
/// </summary>
public class GetProductRequest
{
    /// <summary>
    /// The unique identifier of the Product to retrieve
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// The unique identifier of the Product to retrieve
    /// </summary>
    public int? Size { get; set; }

    /// <summary>
    /// The unique identifier of the Product to retrieve
    /// </summary>
    public string? Order { get; set; }
}