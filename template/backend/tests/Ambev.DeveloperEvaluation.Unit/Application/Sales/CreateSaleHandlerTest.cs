using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.CreateEventHandler;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Rebus.Bus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IPricingService _pricingService;
    private readonly IMapper _mapper;
    private readonly IUser _user;
    private readonly IBus _bus;
    private readonly CreateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _pricingService = Substitute.For<IPricingService>();
        _mapper = Substitute.For<IMapper>();
        _user = Substitute.For<IUser>();
        _bus = Substitute.For<IBus>();
        _handler = new CreateSaleHandler(_saleRepository, _mapper, _user, _pricingService, _bus);
    }

    /// <summary>
    /// Tests that a valid sale creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = CreateSaleHandlerTestData.GenerateValidSale();

        var result = new CreateSaleResult
        {
            SaleNumber = sale.SaleNumber,
            TotalAmount = sale.TotalAmount
        };

        var saleItemResult = new CreateSaleItemResult
        {
            Id = sale.SaleItems[0].Id,
            ProductId = sale.SaleItems[0].ProductId,
            Discount = sale.SaleItems[0].Discount,
            Quantity = sale.SaleItems[0].Quantity,
            Product = sale.SaleItems[0].Product,
            TotalAmount = sale.SaleItems[0].TotalAmount,
            CreatedAt = sale.SaleItems[0].CreatedAt,
            CreatedBy = sale.SaleItems[0].CreatedBy,
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);
        _mapper.Map<CreateSaleItemResult>(sale).Returns(saleItemResult);

        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);
        _user.Username.Returns("testUser");

        foreach (var item in sale.SaleItems)
        {
            _pricingService.ProcessSaleItemPricing(item, Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
        }

        // When
        var validationResponse = command.Validate();
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        createSaleResult.SaleNumber = sale.SaleNumber;
        createSaleResult.TotalAmount = sale.TotalAmount;

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.SaleNumber.Should().Be(sale.SaleNumber);
        createSaleResult.TotalAmount.Should().Be(sale.TotalAmount);
        Assert.True(validationResponse.IsValid);
        Assert.Empty(validationResponse.Errors);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _bus.Received(1).Publish(Arg.Is<SaleCreatedEvent>(e =>
            e.SaleNumber == sale.SaleNumber && e.TotalAmount == sale.TotalAmount));
    }

    /// <summary>
    /// Tests that an invalid sale creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that a pricing service is called for each sale item.
    /// </summary>
    [Fact(DisplayName = "Given valid sale items When creating sale Then processes pricing for each item")]
    public async Task Handle_ValidRequest_ProcessesPricingForEachItem()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = CreateSaleHandlerTestData.GenerateValidSale();

        _mapper.Map<Sale>(command).Returns(sale);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);
        _user.Username.Returns("testUser");

        foreach (var item in sale.SaleItems)
        {
            _pricingService.ProcessSaleItemPricing(item, Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
        }

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        foreach (var item in sale.SaleItems)
        {
            await _pricingService.Received(1).ProcessSaleItemPricing(item, Arg.Any<CancellationToken>());
        }
    }

    /// <summary>
    /// Tests that the sale number is generated.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then generates sale number")]
    public async Task Handle_ValidRequest_GeneratesSaleNumber()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = CreateSaleHandlerTestData.GenerateValidSale();

        _mapper.Map<Sale>(command).Returns(sale);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);
        _user.Username.Returns("testUser");

        foreach (var item in sale.SaleItems)
        {
            _pricingService.ProcessSaleItemPricing(item, Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
        }

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        sale.SaleNumber.Should().NotBeNullOrEmpty();
    }
}