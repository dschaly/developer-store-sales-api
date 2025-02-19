using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

/// <summary>
/// Profile for mapping GetProduct feature requests to commands
/// </summary>
public class ListProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct feature
    /// </summary>
    public ListProductProfile()
    {
        CreateMap<ListProductRequest, ListProductCommand>();
        CreateMap<ProductResult, ProductResponse>();
    }
}