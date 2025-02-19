namespace Ambev.DeveloperEvaluation.Application.Products.GetProductCategory;

/// <summary>
/// Response model for GetProductCategory operation
/// </summary>
public class GetProductCategoryResult
{
    /// <summary>
    /// The Product's Categories List
    /// </summary>
    public string[]? Categories { get; set; }
}