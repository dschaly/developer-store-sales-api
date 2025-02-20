using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem.CancelSaleItemEventHandler;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

/// <summary>
/// Handler for processing CancelSaleItemCommand requests
/// </summary>
public class CancelSaleItemItemHandler : IRequestHandler<CancelSaleItemCommand, CancelSaleItemSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IPricingService _pricingService;
    private readonly IMapper _mapper;
    private readonly IUser _user;
    private readonly IBus _bus;

    /// <summary>
    /// Initializes a new instance of CancelSaleItemItemHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    public CancelSaleItemItemHandler(ISaleRepository saleRepository, IUser user, IBus bus, ISaleItemRepository saleItemRepository, IPricingService pricingService, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _user = user;
        _bus = bus;
        _saleItemRepository = saleItemRepository;
        _pricingService = pricingService;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CancelSaleItemCommand request
    /// </summary>
    /// <param name="request">The CancelSaleItemCommand command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the sale canceling operation</returns>
    public async Task<CancelSaleItemSaleResult> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new CancelSaleItemCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var saleItem = await _saleItemRepository.GetByIdAsync(request.SaleItemId, cancellationToken)
            ?? throw new InvalidOperationException($"Sale with id {request.SaleItemId} not found");

        var sale = saleItem.Sale;

        var oldTotalAmout = sale.TotalAmount;

        sale.SaleItems.Remove(saleItem);

        foreach (var item in sale.SaleItems)
        {
            item.UpdatedAt = DateTime.UtcNow;
            item.UpdatedBy = _user.Username;
            await _pricingService.ProcessSaleItemPricing(item, cancellationToken);
        }

        sale.CalculateTotalAmout();

        sale.UpdatedAt = DateTime.UtcNow;
        sale.UpdatedBy = _user.Username;

        var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);

        await _bus.Publish(new SaleItemCanceledEvent(updatedSale.SaleNumber, oldTotalAmout, updatedSale.TotalAmount, DateTime.UtcNow));

        return _mapper.Map<CancelSaleItemSaleResult>(updatedSale);
    }
}
