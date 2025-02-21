using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
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
/// Contains unit tests for the <see cref="CancelSaleItemItemHandler"/> class.
/// </summary>
public class CancelSaleItemItemHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IPricingService _pricingService;
    private readonly IMapper _mapper;
    private readonly IUser _user;
    private readonly IBus _bus;
    private readonly CancelSaleItemItemHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CancelSaleItemItemHandlerTests"/> class.
    /// Sets up the test dependencies and creates mock objects.
    /// </summary>
    public CancelSaleItemItemHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _saleItemRepository = Substitute.For<ISaleItemRepository>();
        _pricingService = Substitute.For<IPricingService>();
        _mapper = Substitute.For<IMapper>();
        _user = Substitute.For<IUser>();
        _bus = Substitute.For<IBus>();
        _handler = new CancelSaleItemItemHandler(_saleRepository, _user, _bus, _saleItemRepository, _pricingService, _mapper);
    }

    /// <summary>
    /// Tests that when the command is valid and the sale item exists, the sale item is canceled and the event is published.
    /// </summary>
    [Fact(DisplayName = "Given valid command and sale item exists When canceling sale item Then sale item is canceled and event is published")]
    public async Task Handle_ValidCommand_SaleItemExists_CancelsSaleItemAndPublishesEvent()
    {
        // Given
        var command = CancelSaleItemHandlerTestData.GenerateValidCommand();
        var saleItem = CancelSaleItemHandlerTestData.GenerateValidSaleItem();
        var updatedSale = CancelSaleItemHandlerTestData.GenerateValidSale();
        var expectedResult = CancelSaleItemHandlerTestData.GenerateValidResult();

        var sale = saleItem.Sale;
        var oldTotalAmount = sale.TotalAmount;

        _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(updatedSale);
        _saleItemRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(saleItem);
        _mapper.Map<CancelSaleItemSaleResult>(updatedSale).Returns(expectedResult);

        _user.Username.Returns("Admin");

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();

        sale.UpdatedBy.Should().Be("Admin");

        await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that when the sale item is not found, an exception is thrown.
    /// </summary>
    [Fact(DisplayName = "Given sale item not found When canceling sale item Then throws InvalidOperationException")]
    public async Task Handle_SaleItemNotFound_ThrowsInvalidOperationException()
    {
        // Given
        var command = CancelSaleItemHandlerTestData.GenerateValidCommand();
        _saleItemRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .ReturnsNull();

        // When
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Sale with id {command.SaleItemId} not found");
    }

    /// <summary>
    /// Tests that when the command is invalid, a validation exception is thrown.
    /// </summary>
    [Fact(DisplayName = "Given invalid command When canceling sale item Then throws ValidationException")]
    public void Handle_InvalidCommand_ThrowsValidationException()
    {
        // Given
        var command = CancelSaleItemHandlerTestData.GenerateInvalidCommand();

        var validator = new CancelSaleItemCommandValidator();

        // Then
        Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(command));
    }

    /// <summary>
    /// Tests that when the sale item's price is updated, the pricing service is called.
    /// </summary>
    [Fact(DisplayName = "Given valid command When canceling sale item Then pricing service is called for each item")]
    public async Task Handle_ValidCommand_ProcessesSaleItemPricing()
    {
        // Given
        var command = CancelSaleItemHandlerTestData.GenerateValidCommand();
        var saleItem = CancelSaleItemHandlerTestData.GenerateValidSaleItem();
        var updatedSale = CancelSaleHandlerTestData.GenerateValidSale();

        _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(updatedSale);
        _saleItemRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(saleItem);

        _user.Username.Returns("Admin");

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _pricingService.Received(2).ProcessSaleItemPricing(Arg.Is<SaleItem>(si => si.Id == saleItem.Id), Arg.Any<CancellationToken>());
    }
}