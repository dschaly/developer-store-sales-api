using Ambev.DeveloperEvaluation.Application.Customers.UpdateCustomer;
using Ambev.DeveloperEvaluation.Common.Security;
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
/// Contains unit tests for the <see cref="UpdateCustomerHandler"/> class.
/// </summary>
public class UpdateCustomerHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IUser _user;
    private readonly UpdateCustomerHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCustomerHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public UpdateCustomerHandlerTests()
    {
        _customerRepository = Substitute.For<ICustomerRepository>();
        _mapper = Substitute.For<IMapper>();
        _user = Substitute.For<IUser>();

        _handler = new UpdateCustomerHandler(_customerRepository, _mapper, _user);
    }

    /// <summary>
    /// Tests that a valid UpdateCustomerCommand is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid update command When updating customer Then returns updated customer details")]
    public async Task Handle_ValidRequest_ReturnsUpdatedCustomerDetails()
    {
        // Given
        var command = UpdateCustomerHandlerTestData.GenerateValidCommand();
        var existingCustomer = UpdateCustomerHandlerTestData.GenerateExistingCustomer();
        var updatedCustomer = UpdateCustomerHandlerTestData.GenerateUpdatedCustomer();
        var expectedResult = UpdateCustomerHandlerTestData.GenerateValidResult();

        _customerRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingCustomer);
        _mapper.Map(command, existingCustomer).Returns(updatedCustomer);
        _user.Username.Returns("admin");
        _customerRepository.UpdateAsync(existingCustomer, Arg.Any<CancellationToken>()).Returns(updatedCustomer);
        _mapper.Map<UpdateCustomerResult>(updatedCustomer).Returns(expectedResult);
        var validationResponse = command.Validate();
        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(expectedResult.Id);
        result.Name.Should().Be(expectedResult.Name);
        result.Email.Should().Be(expectedResult.Email);
        Assert.True(validationResponse.IsValid);
        Assert.Empty(validationResponse.Errors);
        await _customerRepository.Received(1).UpdateAsync(existingCustomer, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid UpdateCustomerCommand throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid update command When updating customer Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = UpdateCustomerHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that updating a non-existing customer throws an InvalidOperationException.
    /// </summary>
    [Fact(DisplayName = "Given non-existing customer ID When updating customer Then throws invalid operation exception")]
    public async Task Handle_NonExistingCustomer_ThrowsInvalidOperationException()
    {
        // Given
        var command = UpdateCustomerHandlerTestData.GenerateValidCommand();

        _customerRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).ReturnsNull();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Entity with id {command.Id} not found");
    }
}