using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Profile for mapping between Sale entity and GetSaleResult
/// </summary>
public class ListSaleProfile : Profile
{
    public ListSaleProfile()
    {
        CreateMap<Sale, SaleResult>();
    }
}