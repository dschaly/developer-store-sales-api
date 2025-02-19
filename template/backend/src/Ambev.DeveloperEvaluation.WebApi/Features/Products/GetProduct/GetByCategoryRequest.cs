namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Request model for getting a product list
/// </summary>
public class GetByCategoryRequest
{
    /// <summary>
    /// The Product's category
    /// </summary>
    public required string Category { get; set; }
}