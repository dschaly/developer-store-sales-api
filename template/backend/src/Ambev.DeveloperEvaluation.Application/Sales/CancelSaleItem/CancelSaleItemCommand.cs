using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

/// <summary>
/// Command for canceling a Sale Item
/// </summary>
public class CancelSaleItemCommand : IRequest<CancelSaleItemSaleResult>
{
    /// <summary>
    /// The unique identifier of the Sale Item to delete
    /// </summary>
    public Guid SaleItemId { get; }


    /// <summary>
    /// Initializes a new instance of CancelSaleItemCommand
    /// </summary>
    /// <param name="saleItemid">The ID of the Sale Item to delete</param>
    public CancelSaleItemCommand(Guid saleItemid)
    {
        SaleItemId = saleItemid;
    }
}