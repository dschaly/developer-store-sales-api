using Microsoft.Extensions.Logging;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem.CancelSaleItemEventHandler;

public class SaleItemCanceledEventHandler : IHandleMessages<SaleItemCanceledEvent>
{
    private readonly ILogger<SaleItemCanceledEventHandler> _logger;

    public SaleItemCanceledEventHandler(ILogger<SaleItemCanceledEventHandler> logger)
    {
        _logger = logger;
        _logger.LogInformation("🔥 SaleCancelEventHandler registered!");
    }

    public async Task Handle(SaleItemCanceledEvent message)
    {
        _logger.LogInformation("✅ Sale Item Canceled: Number={SaleNumber}, Previous TotalAmout=R${OldTotalAmout}, New TotalAmount=R${TotalAmount}, CanceledAt={UpdateAt}",
            message.SaleNumber, message.OldTotalAmount, message.TotalAmount, message.UpdatedAt);

        await Task.CompletedTask;
    }
}