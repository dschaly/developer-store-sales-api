using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

/// <summary>
/// Profile for mapping GetProduct feature requests to commands
/// </summary>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct feature
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<GetProductRequest, GetProductCommand>();
        CreateMap<ProductResult, ProductResponse>();
    }
}