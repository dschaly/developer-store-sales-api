using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="user">The Authenticated User</param>
    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IUser user, IDiscountService discountService, IProductRepository productRepository)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _user = user;
        _discountService = discountService;
        _productRepository = productRepository;
    }

    /// <summary>
    /// Handles the CreateSaleCommand request
    /// </summary>
    /// <param name="command">The command to be processed</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Sale Id</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = _mapper.Map<Sale>(command);

        sale.GenerateSaleNumber();
        sale.CreatedBy = _user.Username;

        foreach (var item in sale.SaleItems)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken)
                ?? throw new KeyNotFoundException($"Product with ID {item.ProductId} not found");

            item.CreatedBy = _user.Username;
            var itemDiscount = _discountService.CalculateDiscount(item.Quantity, product.Price);
            item.ApplyDiscount(itemDiscount, product.Price);
            item.CalculateTotalAmount(product.Price);
        }

        sale.CalculateTotalAmout();

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        var result = _mapper.Map<CreateSaleResult>(createdSale);

        return result;
    }
}