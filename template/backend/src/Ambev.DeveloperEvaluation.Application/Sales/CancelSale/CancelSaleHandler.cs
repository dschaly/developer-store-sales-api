using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handler for processing CancelSaleCommand requests
/// </summary>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResponse>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUser _user;

    /// <summary>
    /// Initializes a new instance of CancelSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    public CancelSaleHandler(ISaleRepository saleRepository, IUser user)
    {
        _saleRepository = saleRepository;
        _user = user;
    }

    /// <summary>
    /// Handles the CancelSaleCommand request
    /// </summary>
    /// <param name="request">The CancelSaleCommand command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the sale canceling operation</returns>
    public async Task<CancelSaleResponse> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CancelSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Sale with id {request.Id} not found");

        sale.CancelSale();
        sale.UpdatedAt = DateTime.UtcNow;
        sale.UpdatedBy = _user.Username;

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        return new CancelSaleResponse { Success = true };
    }
}
