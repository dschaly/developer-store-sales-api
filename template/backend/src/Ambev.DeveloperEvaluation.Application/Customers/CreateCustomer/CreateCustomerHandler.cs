﻿using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

/// <summary>
/// Handler for processing CreateCustomerCommand requests
/// </summary>
public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResult>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    /// <summary>
    /// Initializes a new instance of CreateCustomerHandler
    /// </summary>
    /// <param name="customerRepository">The customer repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="user">The Authenticated User</param>
    public CreateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper, IUser user)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _user = user;
    }

    /// <summary>
    /// Handles the CreateCustomerCommand request
    /// </summary>
    /// <param name="command">The command to be processed</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Customer Id</returns>
    public async Task<CreateCustomerResult> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateCustomerCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingCustomer = await _customerRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (existingCustomer is not null)
            throw new InvalidOperationException($"Customer with e-mail {command.Email} already exists");

        var customer = _mapper.Map<Customer>(command);

        customer.CreatedBy = _user.Username;

        var createdCustomer = await _customerRepository.CreateAsync(customer, cancellationToken);
        var result = _mapper.Map<CreateCustomerResult>(createdCustomer);

        return result;
    }
}
