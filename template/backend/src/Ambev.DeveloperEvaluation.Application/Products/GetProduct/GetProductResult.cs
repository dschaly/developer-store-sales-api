using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Response model for GetProduct operation
/// </summary>
public class GetProductResult : BaseResult
{
    /// <summary>
    /// The Product's name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The Product's price
    /// </summary>
    public decimal Price { get; set; }
}