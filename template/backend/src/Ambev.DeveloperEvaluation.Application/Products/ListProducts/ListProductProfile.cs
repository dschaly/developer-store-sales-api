using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

/// <summary>
/// Profile for mapping between Product entity and GetProductResult
/// </summary>
public class ListProductProfile : Profile
{
    public ListProductProfile()
    {
        CreateMap<Product, ProductResult>();
    }
}