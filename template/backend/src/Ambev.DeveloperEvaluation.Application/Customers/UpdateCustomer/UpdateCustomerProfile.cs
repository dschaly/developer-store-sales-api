using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Customers.UpdateCustomer;

/// <summary>
/// Profile for mapping between Customer entity and UpdateCustomerResponse
/// </summary>
public class UpdateCustomerProfile : Profile
{
    public UpdateCustomerProfile()
    {
        CreateMap<UpdateCustomerCommand, Customer>()
            .ForMember(o => o.CreatedAt, opt => opt.Ignore())
            .ForMember(o => o.CreatedBy, opt => opt.Ignore())
            .ForMember(o => o.UpdatedAt, opt => opt.Ignore())
            .ForMember(o => o.UpdatedBy, opt => opt.Ignore());
        CreateMap<Customer, UpdateCustomerResult>();
    }
}