namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.UpdateEventHandler;

public class SaleUpdatedEvent
{
    public SaleUpdatedEvent(string saleNumber, decimal totalAmount, DateTime updatedAt)
    {
        SaleNumber = saleNumber;
        TotalAmount = totalAmount;
        UpdatedAt = updatedAt;
    }

    public string SaleNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime UpdatedAt { get; set; }
}