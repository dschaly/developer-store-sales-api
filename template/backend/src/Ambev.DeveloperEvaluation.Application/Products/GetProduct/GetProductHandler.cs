using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public class GetProductHandler : IRequestHandler<GetProductCommand, GetProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetProductResult> Handle(GetProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new GetProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        int page = command.Page ?? 1;
        int size = command.Size ?? 10;

        var query = await _productRepository.Query(cancellationToken);

        query = ApplyFiltering(query, command);

        if (!string.IsNullOrWhiteSpace(command.Order))
        {
            query = QueryingUtil<Product>.ApplyOrdering(query, command.Order);
        }

        int totalCount = await query.CountAsync(cancellationToken);

        var products = query.Skip((page - 1) * size).Take(size);

        var result = new GetProductResult
        {
            Data = _mapper.Map<IEnumerable<ProductResult>>(products),
            TotalCount = totalCount,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalCount / (double)size)
        };

        return result;
    }

    public static IQueryable<Product> ApplyFiltering(IQueryable<Product> query, GetProductCommand command)
    {
        if (!string.IsNullOrWhiteSpace(command.Title))
            query = query.Where(p => EF.Functions.Like(EF.Property<string>(p, "Title"), $"%{command.Title}%"));

        if (!string.IsNullOrWhiteSpace(command.Title))
            query = query.Where(p => EF.Functions.Like(EF.Property<string>(p, "Title"), $"%{command.Title}%"));

        return query;
    }
}