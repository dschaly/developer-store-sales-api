namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

/// <summary>
/// Request model for getting a product list
/// </summary>
public class ListByCategoryRequest
{
    /// <summary>
    /// The Product's category
    /// </summary>
    public required string Category { get; set; }
}