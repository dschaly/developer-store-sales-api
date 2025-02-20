namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

/// <summary>
/// Request model for getting a product list
/// </summary>
public class ListSaleRequest
{
    /// <summary>
    /// The unique identifier of the Sale to retrieve
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// The unique identifier of the Sale to retrieve
    /// </summary>
    public int? Size { get; set; }

    /// <summary>
    /// The unique identifier of the Sale to retrieve
    /// </summary>
    public string? Order { get; set; }

    /// <summary>
    /// The Sale's title Filter
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The Sale's category Filter
    /// </summary>
    public string? Category { get; set; }
}