using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

/// <summary>
/// Profile for mapping between Customer entity, CreateCustomerCommand, and CreateCustomerResult
/// </summary>
public class CreateCustomerProfile : Profile
{
    public CreateCustomerProfile()
    {
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<Customer, CreateCustomerResult>();
    }
}