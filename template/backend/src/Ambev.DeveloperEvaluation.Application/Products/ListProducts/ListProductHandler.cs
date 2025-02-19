using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

/// <summary>
/// Handler for processing ListProductCommand requests
/// </summary>
public class ListProductHandler : IRequestHandler<ListProductCommand, ListProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ListProductHandler
    /// </summary>
    /// <param name="productRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ListProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListProductCommand request
    /// </summary>
    /// <param name="command">The ListProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The paginted products list</returns>
    public async Task<ListProductResult> Handle(ListProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        int page = command.Page ?? 1;
        int size = command.Size ?? 10;

        var query = await _productRepository.Query(cancellationToken);

        if (query is null)
        {
            return new ListProductResult
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
            query = QueryingUtil<Product>.ApplyOrdering(query, command.Order);
        }

        int totalCount = await query.CountAsync(cancellationToken);

        var products = query.Skip((page - 1) * size).Take(size);

        var result = new ListProductResult
        {
            Data = _mapper.Map<IEnumerable<ProductResult>>(products),
            TotalCount = totalCount,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalCount / (double)size)
        };

        return result;
    }

    public static IQueryable<Product> ApplyFiltering(IQueryable<Product> query, ListProductCommand command)
    {
        if (!string.IsNullOrWhiteSpace(command.Title))
            query = query.Where(p => EF.Functions.Like(EF.Property<string>(p, "Title"), $"%{command.Title}%"));

        if (!string.IsNullOrWhiteSpace(command.Category))
            query = query.Where(p => EF.Functions.Like(EF.Property<string>(p, "Category"), $"%{command.Category}%"));

        return query;
    }
}