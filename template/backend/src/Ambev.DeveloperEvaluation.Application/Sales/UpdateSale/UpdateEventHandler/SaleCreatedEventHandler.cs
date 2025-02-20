using Microsoft.Extensions.Logging;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.UpdateEventHandler;

public class SaleUpdateEventHandler : IHandleMessages<SaleUpdatedEvent>
{
    private readonly ILogger<SaleUpdateEventHandler> _logger;

    public SaleUpdateEventHandler(ILogger<SaleUpdateEventHandler> logger)
    {
        _logger = logger;
        _logger.LogInformation("🔥 SaleUpdateEventHandler registered!");
    }

    public async Task Handle(SaleUpdatedEvent message)
    {
        _logger.LogInformation("✅ Sale Updated: Number={SaleNumber}, Amount=R${TotalAmount}, UpdatedAt={UpdateAt}",
            message.SaleNumber, message.TotalAmount, message.UpdatedAt);

        await Task.CompletedTask;
    }
}