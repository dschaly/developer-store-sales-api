using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Command for retrieving a list of all sales 
/// </summary>
public class ListSaleCommand : IRequest<ListSaleResult>
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