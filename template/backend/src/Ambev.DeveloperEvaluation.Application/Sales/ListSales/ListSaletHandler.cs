using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Handler for processing ListSaleCommand requests
/// </summary>
public class ListSaleHandler : IRequestHandler<ListSaleCommand, ListSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ListSaleHandler
    /// </summary>
    /// <param name="saleRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ListSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListSaleCommand request
    /// </summary>
    /// <param name="command">The ListSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The paginted sales list</returns>
    public async Task<ListSaleResult> Handle(ListSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        int page = command.Page ?? 1;
        int size = command.Size ?? 10;

        var query = await _saleRepository.Query(cancellationToken);

        if (query is null)
        {
            return new ListSaleResult
            {
                Data = [],
                TotalCount = 0,
                CurrentPage = page,
                TotalPages = 0
            };
        }

        query = ApplyFiltering(query, command);

        if (!string.IsNullOrWhiteSpace(command.Order))
        {
            query = QueryingUtil<Sale>.ApplyOrdering(query, command.Order);
        }

        int totalCount = await query.CountAsync(cancellationToken);

        var sales = query.Skip((page - 1) * size).Take(size);

        var result = new ListSaleResult
        {
            Data = _mapper.Map<IEnumerable<SaleResult>>(sales),
            TotalCount = totalCount,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalCount / (double)size)
        };

        return result;
    }

    public static IQueryable<Sale> ApplyFiltering(IQueryable<Sale> query, ListSaleCommand command)
    {
        if (!string.IsNullOrWhiteSpace(command.Title))
            query = query.Where(p => EF.Functions.Like(EF.Property<string>(p, "Title"), $"%{command.Title}%"));

        if (!string.IsNullOrWhiteSpace(command.Category))
            query = query.Where(p => EF.Functions.Like(EF.Property<string>(p, "Category"), $"%{command.Category}%"));

        return query;
    }
}