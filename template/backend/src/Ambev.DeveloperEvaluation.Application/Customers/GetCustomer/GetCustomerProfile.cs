﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

/// <summary>
/// Profile for mapping between Customer entity and GetCustomerResult
/// </summary>
public class GetCustomerProfile : Profile
{
    public GetCustomerProfile()
    {
        CreateMap<Customer, GetCustomerResult>();
    }
}