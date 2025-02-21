using Ambev.DeveloperEvaluation.Application.Customers.DeleteCustomer;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Customers;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customers;

/// <summary>
/// Contains unit tests for the <see cref="DeleteCustomerHandler"/> class.
/// </summary>
public class DeleteCustomerHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly DeleteCustomerHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCustomerHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public DeleteCustomerHandlerTests()
    {
        _customerRepository = Substitute.For<ICustomerRepository>();
        _handler = new DeleteCustomerHandler(_customerRepository);
    }

    /// <summary>
    /// Tests that a valid DeleteCustomerCommand is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid customer ID When deleting customer Then returns success")]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        // Given
        var command = DeleteCustomerHandlerTestData.GenerateValidCommand();
        _customerRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(true);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        await _customerRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid DeleteCustomerCommand throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid customer ID When deleting customer Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = DeleteCustomerHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that deleting a non-existing customer throws a KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existing customer ID When deleting customer Then throws key not found exception")]
    public async Task Handle_NonExistingCustomer_ThrowsKeyNotFoundException()
    {
        // Given
        var command = DeleteCustomerHandlerTestData.GenerateValidCommand();
        _customerRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(false);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Customer with ID {command.Id} not found");
    }
}