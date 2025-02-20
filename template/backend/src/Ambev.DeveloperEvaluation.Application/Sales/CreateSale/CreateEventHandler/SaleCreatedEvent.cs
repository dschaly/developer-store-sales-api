namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.CreateEventHandler;

public class SaleCreatedEvent
{
    public SaleCreatedEvent(string saleNumber, decimal totalAmount, DateTime createdAt)
    {
        SaleNumber = saleNumber;
        TotalAmount = totalAmount;
        CreatedAt = createdAt;
    }

    public string SaleNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}