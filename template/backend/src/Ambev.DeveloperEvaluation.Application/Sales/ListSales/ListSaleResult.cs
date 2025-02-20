using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Response model for GetSale operation
/// </summary>
public class ListSaleResult
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<SaleResult>? Data { get; set; }
}

public class SaleResult : BaseResult
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