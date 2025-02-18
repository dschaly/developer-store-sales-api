using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

/// <summary>
/// Command for retrieving a customer by their ID
/// </summary>
public class GetCustomerCommand : IRequest<GetCustomerResult>
{
    /// <summary>
    /// The unique identifier of the Customer to retrieve
    /// </summary>
    public Guid Id { get; }
    /// <summary>
    /// Initializes a new instance of GetCustomerCommand
    /// </summary>
    /// <param name="id">The ID of the Customer to retrieve</param>
    public GetCustomerCommand(Guid id)
    {
        Id = id;
    }
}