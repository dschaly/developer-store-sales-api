using Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Customers;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customers;

/// <summary>
/// Contains unit tests for the <see cref="GetCustomerHandler"/> class.
/// </summary>
public class GetCustomerHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly GetCustomerHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCustomerHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public GetCustomerHandlerTests()
    {
        _customerRepository = Substitute.For<ICustomerRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetCustomerHandler(_customerRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid GetCustomerCommand is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid customer ID When retrieving customer Then returns customer details")]
    public async Task Handle_ValidRequest_ReturnsCustomerDetails()
    {
        // Given
        var command = GetCustomerHandlerTestData.GenerateValidCommand();
        var customer = GetCustomerHandlerTestData.GenerateValidCustomer();
        var expectedResult = GetCustomerHandlerTestData.GenerateValidResult();

        _customerRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(customer);
        _mapper.Map<GetCustomerResult>(customer).Returns(expectedResult);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(expectedResult.Id);
        result.Name.Should().Be(expectedResult.Name);
        result.Email.Should().Be(expectedResult.Email);
        await _customerRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid GetCustomerCommand throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid customer ID When retrieving customer Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new GetCustomerCommand(Guid.Empty); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that retrieving a non-existing customer throws a KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existing customer ID When retrieving customer Then throws key not found exception")]
    public async Task Handle_NonExistingCustomer_ThrowsKeyNotFoundException()
    {
        // Given
        var command = GetCustomerHandlerTestData.GenerateValidCommand();

        _customerRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).ReturnsNull();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Customer with ID {command.Id} not found");
    }
}
