using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductCategory;

/// <summary>
/// Handler for processing GetProductCategoryCommand requests
/// </summary>
public class GetProductCategoryHandler : IRequestHandler<GetProductCategoryCommand, GetProductCategoryResult>
{
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of GetProductCategoryHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    public GetProductCategoryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    /// <summary>
    /// Handles the GetProductCategoryCommand request
    /// </summary>
    /// <param name="command">The GetProductCategory command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The procucts paginted list by category</returns>
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