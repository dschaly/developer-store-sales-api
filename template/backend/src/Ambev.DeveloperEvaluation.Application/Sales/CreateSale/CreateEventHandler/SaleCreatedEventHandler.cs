using Microsoft.Extensions.Logging;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.CreateEventHandler;

public class SaleCreatedEventHandler : IHandleMessages<SaleCreatedEvent>
{
    private readonly ILogger<SaleCreatedEventHandler> _logger;

    public SaleCreatedEventHandler(ILogger<SaleCreatedEventHandler> logger)
    {
        _logger = logger;
        _logger.LogInformation("🔥 SaleCreatedEventHandler registered!");
    }

    public async Task Handle(SaleCreatedEvent message)
    {
        _logger.LogInformation("✅ Sale Created: Number={SaleNumber}, Amount=R${TotalAmount}, CreatedAt={CreatedAt}",
            message.SaleNumber, message.TotalAmount, message.CreatedAt);

        await Task.CompletedTask;
    }
}