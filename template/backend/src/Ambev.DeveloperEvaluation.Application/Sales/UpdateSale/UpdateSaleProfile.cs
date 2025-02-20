using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Profile for mapping between Sale entity and UpdateSaleResponse
/// </summary>
public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleCommand, Sale>()
            .ForMember(o => o.SaleNumber, opt => opt.Ignore())
            .ForMember(o => o.TotalAmount, opt => opt.Ignore())
            .ForMember(o => o.IsCancelled, opt => opt.Ignore())
            .ForMember(o => o.CreatedAt, opt => opt.Ignore())
            .ForMember(o => o.CreatedBy, opt => opt.Ignore())
            .ForMember(o => o.UpdatedAt, opt => opt.Ignore())
            .ForMember(o => o.UpdatedBy, opt => opt.Ignore());
        CreateMap<Sale, UpdateSaleResult>();
        CreateMap<SaleItem, UpdateSaleItemResult>();
    }
}