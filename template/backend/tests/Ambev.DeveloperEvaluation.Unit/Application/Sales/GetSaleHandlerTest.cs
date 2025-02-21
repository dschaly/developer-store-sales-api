using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="GetSaleHandler"/> class.
/// </summary>
public class GetSaleHandlerTests
{
    private readonly ISaleRepository _branchRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public GetSaleHandlerTests()
    {
        _branchRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleHandler(_branchRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid GetSaleCommand is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid branch ID When retrieving branch Then returns branch details")]
    public async Task Handle_ValidRequest_ReturnsSaleDetails()
    {
        // Given
        var command = GetSaleHandlerTestData.GenerateValidCommand();
        var branch = GetSaleHandlerTestData.GenerateValidSale();
        var expectedResult = GetSaleHandlerTestData.GenerateValidResult();

        _branchRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(branch);
        _mapper.Map<GetSaleResult>(branch).Returns(expectedResult);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(expectedResult.Id);
        result.BranchId.Should().Be(expectedResult.BranchId);
        result.CustomerId.Should().Be(expectedResult.CustomerId);
        await _branchRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid GetSaleCommand throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid branch ID When retrieving branch Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new GetSaleCommand(Guid.Empty); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that retrieving a non-existing branch throws a KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existing branch ID When retrieving branch Then throws key not found exception")]
    public async Task Handle_NonExistingSale_ThrowsKeyNotFoundException()
    {
        // Given
        var command = GetSaleHandlerTestData.GenerateValidCommand();

        _branchRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).ReturnsNull();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Sale with ID {command.Id} not found");
    }
}
