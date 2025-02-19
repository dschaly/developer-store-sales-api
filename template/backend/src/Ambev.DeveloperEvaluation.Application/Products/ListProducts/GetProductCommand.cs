using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

/// <summary>
/// Command for retrieving a list of all product 
/// </summary>
public class GetProductCommand : IRequest<GetProductResult>
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

    /// <summary>
    /// The Product's title Filter
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The Product's category Filter
    /// </summary>
    public string? Category { get; set; }
}