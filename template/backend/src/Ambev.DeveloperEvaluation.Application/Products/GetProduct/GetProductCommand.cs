using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

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
}