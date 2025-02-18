using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Profile for mapping between Product entity and UpdateProductResponse
/// </summary>
public class UpdateProductProfile : Profile
{
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductCommand, Product>()
            .ForMember(o => o.CreatedAt, opt => opt.Ignore())
            .ForMember(o => o.CreatedBy, opt => opt.Ignore())
            .ForMember(o => o.UpdatedAt, opt => opt.Ignore())
            .ForMember(o => o.UpdatedBy, opt => opt.Ignore());
        CreateMap<Product, UpdateProductResult>();
    }
}