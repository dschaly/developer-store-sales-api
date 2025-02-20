using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

/// <summary>
/// Profile for mapping GetSale feature requests to commands
/// </summary>
public class ListSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSale feature
    /// </summary>
    public ListSaleProfile()
    {
        CreateMap<ListSaleRequest, ListSaleCommand>();
        CreateMap<SaleResult, SaleResponse>();
        CreateMap<ListSaleItemResult, ListSaleItemResponse>();
    }
}