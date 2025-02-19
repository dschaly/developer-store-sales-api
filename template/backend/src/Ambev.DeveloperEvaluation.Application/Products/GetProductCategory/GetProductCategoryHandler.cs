using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductCategory;

public class GetProductCategoryHandler : IRequestHandler<GetProductCategoryCommand, GetProductCategoryResult>
{
    private readonly IProductRepository _productRepository;

    public GetProductCategoryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetProductCategoryResult> Handle(GetProductCategoryCommand command, CancellationToken cancellationToken)
    {
        var result = new GetProductCategoryResult();

        var query = await _productRepository.Query(cancellationToken);

        if (query is not null)
        {
            result.Categories = await query.Select(p => p.Category).Distinct().ToArrayAsync(cancellationToken);
        }

        return result;
    }
}