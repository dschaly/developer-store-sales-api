using Microsoft.Extensions.Logging;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale.CancelSaleEventHandler;

public class SaleCanceledEventHandler : IHandleMessages<SaleCanceledEvent>
{
    private readonly ILogger<SaleCanceledEventHandler> _logger;

    public SaleCanceledEventHandler(ILogger<SaleCanceledEventHandler> logger)
    {
        _logger = logger;
        _logger.LogInformation("🔥 SaleCancelEventHandler registered!");
    }

    public async Task Handle(SaleCanceledEvent message)
    {
        _logger.LogInformation("✅ Sale Canceled: Number={SaleNumber}, Amount=R${TotalAmount}, CanceledAt={UpdateAt}",
            message.SaleNumber, message.TotalAmount, message.UpdatedAt);

        await Task.CompletedTask;
    }
}