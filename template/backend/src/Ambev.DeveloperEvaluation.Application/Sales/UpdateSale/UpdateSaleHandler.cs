using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.UpdateEventHandler;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for processing UpdateSaleCommand requests
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IPricingService _pricingService;
    private readonly IMapper _mapper;
    private readonly IUser _user;
    private readonly IBus _bus;

    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="user">The Authenticated User</param>
    public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IUser user, IPricingService pricingService, IBus bus)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _user = user;
        _pricingService = pricingService;
        _bus = bus;
    }

    /// <summary>
    /// Handles the UpdateSaleCommand request
    /// </summary>
    /// <param name="command">The UpdateSaleCommand command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale details</returns>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Sale with id {command.Id} not found");

        _mapper.Map(command, sale);

        foreach (var item in sale.SaleItems)
        {
            item.SaleId = sale.Id;
            item.CreatedBy ??= _user.Username;
            item.UpdatedBy = _user.Username;
            item.UpdatedAt = DateTime.UtcNow;
            await _pricingService.ProcessSaleItemPricing(item, cancellationToken);
        }

        sale.CalculateTotalAmout();

        sale.UpdatedBy = _user.Username;
        sale.UpdatedAt = DateTime.UtcNow;
        var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);

        await _bus.Publish(new SaleUpdatedEvent(updatedSale.SaleNumber, updatedSale.TotalAmount, updatedSale.CreatedAt));

        var result = _mapper.Map<UpdateSaleResult>(updatedSale);

        return result;
    }
}