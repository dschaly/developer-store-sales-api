using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Customers;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customers;

/// <summary>
/// Contains unit tests for the <see cref="CreateCustomerHandler"/> class.
/// </summary>
public class CreateCustomerHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IUser _user;
    private readonly CreateCustomerHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCustomerHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateCustomerHandlerTests()
    {
        _customerRepository = Substitute.For<ICustomerRepository>();
        _mapper = Substitute.For<IMapper>();
        _user = Substitute.For<IUser>();
        _handler = new CreateCustomerHandler(_customerRepository, _mapper, _user);
    }

    /// <summary>
    /// Tests that a valid customer creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid customer data When creating customer Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateCustomerHandlerTestData.GenerateValidCommand();
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            CreatedBy = "testUser"
        };

        var result = new CreateCustomerResult
        {
            Id = customer.Id,
        };

        _mapper.Map<Customer>(command).Returns(customer);
        _mapper.Map<CreateCustomerResult>(customer).Returns(result);

        _customerRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns((Customer?)null);
        _customerRepository.CreateAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>())
            .Returns(customer);

        _user.Username.Returns("testUser");

        // When
        var createCustomerResult = await _handler.Handle(command, CancellationToken.None);
        var validationResponse = command.Validate();

        // Then
        createCustomerResult.Should().NotBeNull();
        createCustomerResult.Id.Should().Be(customer.Id);
        Assert.True(validationResponse.IsValid);
        Assert.Empty(validationResponse.Errors);
        await _customerRepository.Received(1).CreateAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid customer creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid customer data When creating customer Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateCustomerCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that an attempt to create a customer with an existing email throws an exception.
    /// </summary>
    [Fact(DisplayName = "Given existing customer email When creating customer Then throws invalid operation exception")]
    public async Task Handle_ExistingEmail_ThrowsInvalidOperationException()
    {
        // Given
        var command = CreateCustomerHandlerTestData.GenerateValidCommand();
        var existingCustomer = new Customer { Email = command.Email, CreatedBy = "testUser" };

        _customerRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(existingCustomer);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Customer with e-mail {command.Email} already exists");
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to customer entity")]
    public async Task Handle_ValidRequest_MapsCommandToCustomer()
    {
        // Given
        var command = CreateCustomerHandlerTestData.GenerateValidCommand();
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            CreatedBy = "testUser"
        };

        _mapper.Map<Customer>(command).Returns(customer);
        _customerRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns((Customer?)null);
        _customerRepository.CreateAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>())
            .Returns(customer);
        _user.Username.Returns("testUser");

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Customer>(Arg.Is<CreateCustomerCommand>(c =>
            c.Email == command.Email));
    }
}