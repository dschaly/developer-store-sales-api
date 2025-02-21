using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using AutoMapper;
using FluentAssertions;
using MockQueryable;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="ListSaleHandler"/> class.
/// </summary>
public class ListSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ListSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListSaleHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public ListSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new ListSaleHandler(_saleRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid ListSaleCommand returns a paginated sale list.
    /// </summary>
    [Fact(DisplayName = "Given valid command When listing sales Then returns paginated result")]
    public async Task Handle_ValidRequest_ReturnsPaginatedSales()
    {
        // Given
        var command = ListSaleHandlerTestData.GenerateValidCommand();
        var sales = ListSaleHandlerTestData.GenerateSaleList();
        var mappedSales = ListSaleHandlerTestData.GenerateMappedSaleList();

        var queryableSales = sales.AsQueryable().BuildMock(); // Using MockQueryable

        _saleRepository.Query(Arg.Any<CancellationToken>()).Returns(queryableSales);
        _mapper.Map<IEnumerable<SaleResult>>(Arg.Any<IEnumerable<Sale>>()).Returns(mappedSales);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Data.Should().NotBeEmpty();
        result.TotalCount.Should().Be(sales.Count);
        result.CurrentPage.Should().Be(command.Page ?? 1);
        result.TotalPages.Should().Be((int)Math.Ceiling(sales.Count / (double)(command.Size ?? 10)));

        await _saleRepository.Received(1).Query(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid ListSaleCommand throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid command When listing sales Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = ListSaleHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    /// <summary>
    /// Tests that an empty sale list returns an empty result.
    /// </summary>
    [Fact(DisplayName = "Given no sales in repository When listing sales Then returns empty result")]
    public async Task Handle_NoSales_ReturnsEmptyResult()
    {
        // Given
        var command = ListSaleHandlerTestData.GenerateValidCommand();
        var emptyQuery = Enumerable.Empty<Sale>().AsQueryable().BuildMock();

        _saleRepository.Query(Arg.Any<CancellationToken>()).Returns(emptyQuery);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Data.Should().BeEmpty();
        result.TotalCount.Should().Be(0);
        result.CurrentPage.Should().Be(command.Page ?? 1);
        result.TotalPages.Should().Be(0);
    }
}