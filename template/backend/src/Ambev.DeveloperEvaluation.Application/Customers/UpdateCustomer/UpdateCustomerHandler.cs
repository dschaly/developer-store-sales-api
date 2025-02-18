using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.UpdateCustomer;

/// <summary>
/// Handler for processing UpdateCustomerCommand requests
/// </summary>
public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResult>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the UpdateCustomerCommand request
    /// </summary>
    /// <param name="command">The UpdateCustomerCommand command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated customer details</returns>
    public async Task<UpdateCustomerResult> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateCustomerCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var entity = await _customerRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Entity with id {command.Id} not found");

        _mapper.Map(command, entity);

        entity.UpdatedAt = DateTime.UtcNow;

        var updatedCustomer = await _customerRepository.UpdateAsync(entity, cancellationToken);

        var result = _mapper.Map<UpdateCustomerResult>(updatedCustomer);

        return result;
    }
}