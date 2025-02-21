using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale.CancelSaleEventHandler;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Rebus.Bus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="CancelSaleHandler"/> class.
/// </summary>
public class CancelSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUser _user;
    private readonly IBus _bus;
    private readonly CancelSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CancelSaleHandlerTests"/> class.
    /// Sets up the test dependencies and creates mock objects.
    /// </summary>
    public CancelSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _user = Substitute.For<IUser>();
        _bus = Substitute.For<IBus>();
        _handler = new CancelSaleHandler(_saleRepository, _user, _bus);
    }

    /// <summary>
    /// Tests that when the command is valid and the sale exists, the sale is canceled and the event is published.
    /// </summary>
    [Fact(DisplayName = "Given valid command and sale exists When canceling sale Then sale is canceled and event is published")]
    public async Task Handle_ValidCommand_SaleExists_CancelsSaleAndPublishesEvent()
    {
        // Given
        var command = CancelSaleHandlerTestData.GenerateValidCommand();

        var sale = CancelSaleHandlerTestData.GenerateValidSale();

        _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        _user.Username.Returns("Admin");

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();

        sale.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        sale.UpdatedBy.Should().Be("Admin");

        await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
        await _bus.Received(1).Publish(Arg.Is<SaleCanceledEvent>(e => e.SaleNumber == sale.SaleNumber && e.TotalAmount == sale.TotalAmount));
    }

    /// <summary>
    /// Tests that when the sale is not found, an exception is thrown.
    /// </summary>
    [Fact(DisplayName = "Given sale not found When canceling sale Then throws InvalidOperationException")]
    public async Task Handle_SaleNotFound_ThrowsInvalidOperationException()
    {
        // Given
        var command = CancelSaleHandlerTestData.GenerateValidCommand();

        _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .ReturnsNull();

        // When
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Sale with id {command.Id} not found");
    }

    /// <summary>
    /// Tests that when the command is invalid, a validation exception is thrown.
    /// </summary>
    [Fact(DisplayName = "Given invalid command When canceling sale Then throws ValidationException")]
    public void Handle_InvalidCommand_ThrowsValidationException()
    {
        // Given
        var command = CancelSaleHandlerTestData.GenerateInvalidCommand();
        var validator = new CancelSaleCommandValidator();

        // Then
        Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(command));
    }

    /// <summary>
    /// Tests that when updating the sale, the 'UpdatedAt' field is properly set.
    /// </summary>
    [Fact(DisplayName = "Given valid command When canceling sale Then 'UpdatedAt' field is set correctly")]
    public async Task Handle_ValidCommand_SetsUpdatedAtField()
    {
        // Given
        var command = CancelSaleHandlerTestData.GenerateValidCommand();
        var sale = CancelSaleHandlerTestData.GenerateValidSale();

        _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        _user.Username.Returns("Admin");

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        sale.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    /// <summary>
    /// Tests that the sale canceling event is published.
    /// </summary>
    [Fact(DisplayName = "Given valid command When canceling sale Then publishes SaleCanceledEvent")]
    public async Task Handle_ValidCommand_PublishesSaleCanceledEvent()
    {
        // Given
        var command = CancelSaleHandlerTestData.GenerateValidCommand();
        var sale = CancelSaleHandlerTestData.GenerateValidSale();

        _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        _user.Username.Returns("Admin");

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _bus.Received(1).Publish(Arg.Is<SaleCanceledEvent>(e => e.SaleNumber == sale.SaleNumber && e.TotalAmount == sale.TotalAmount));
    }
}
