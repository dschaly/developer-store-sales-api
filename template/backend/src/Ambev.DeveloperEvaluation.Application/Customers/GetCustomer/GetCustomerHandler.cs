using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

public class GetCustomerHandler : IRequestHandler<GetCustomerCommand, GetCustomerResult>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<GetCustomerResult> Handle(GetCustomerCommand command, CancellationToken cancellationToken)
    {
        var validator = new GetCustomerCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var customer = await _customerRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Customer with ID {command.Id} not found");

        var result = _mapper.Map<GetCustomerResult>(customer);
        return result;
    }
}