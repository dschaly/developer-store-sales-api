namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem.CancelSaleItemEventHandler;

public class SaleItemCanceledEvent
{
    public SaleItemCanceledEvent(string saleNumber, decimal oldTotalAmount, decimal totalAmount, DateTime updatedAt)
    {
        SaleNumber = saleNumber;
        TotalAmount = totalAmount;
        OldTotalAmount = oldTotalAmount;
        UpdatedAt = updatedAt;
    }

    public string SaleNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal OldTotalAmount { get; set; }
    public DateTime UpdatedAt { get; set; }
}