using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Rebus.Bus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="UpdateSaleHandler"/> class.
/// </summary>
public class UpdateSaleHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IUser _user;
    private readonly IBus _bus;
    private readonly DiscountService _discountService;
    private readonly PricingService _pricingService;
    private readonly UpdateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public UpdateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _user = Substitute.For<IUser>();
        _bus = Substitute.For<IBus>();
        _discountService = new DiscountService();
        _pricingService = new PricingService(_productRepository, _discountService);

        _handler = new UpdateSaleHandler(_saleRepository, _mapper, _user, _pricingService, _bus);
    }

    /// <summary>
    /// Tests that a valid UpdateSaleCommand is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid update command When updating sale Then returns updated sale details")]
    public async Task Handle_ValidRequest_ReturnsUpdatedSaleDetails()
    {
        // Given
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();
        var existingSale = UpdateSaleHandlerTestData.GenerateExistingSale();
        var existingProduct = UpdateSaleHandlerTestData.GenerateExistingProduct();
        var updatedSale = UpdateSaleHandlerTestData.GenerateUpdatedSale();
        var expectedResult = UpdateSaleHandlerTestData.GenerateValidResult();

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingSale);
        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(existingProduct);
        _mapper.Map(command, existingSale).Returns(updatedSale);
        _user.Username.Returns("admin");
        _saleRepository.UpdateAsync(existingSale, Arg.Any<CancellationToken>()).Returns(updatedSale);
        _mapper.Map<UpdateSaleResult>(updatedSale).Returns(expectedResult);

        // When
        var validationResponse = command.Validate();
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(expectedResult.Id);
        result.SaleNumber.Should().Be(expectedResult.SaleNumber);
        result.TotalAmount.Should().Be(expectedResult.TotalAmount);

        Assert.True(validationResponse.IsValid);
        Assert.Empty(validationResponse.Errors);
        await _saleRepository.Received(1).UpdateAsync(existingSale, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid UpdateSaleCommand throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid update command When updating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = UpdateSaleHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that updating a non-existing sale throws an InvalidOperationException.
    /// </summary>
    [Fact(DisplayName = "Given non-existing sale ID When updating sale Then throws invalid operation exception")]
    public async Task Handle_NonExistingSale_ThrowsInvalidOperationException()
    {
        // Given
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).ReturnsNull();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Sale with id {command.Id} not found");
    }
}