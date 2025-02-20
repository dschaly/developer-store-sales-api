using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Command for canceling a Sale
/// </summary>
public class CancelSaleCommand : IRequest<CancelSaleResponse>
{
    /// <summary>
    /// The unique identifier of the Sale to delete
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of CancelSaleCommand
    /// </summary>
    /// <param name="id">The ID of the Sale to delete</param>
    public CancelSaleCommand(Guid id)
    {
        Id = id;
    }
}