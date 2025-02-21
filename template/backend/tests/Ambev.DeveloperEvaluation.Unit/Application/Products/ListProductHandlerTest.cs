using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Products;
using AutoMapper;
using FluentAssertions;
using MockQueryable;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="ListProductHandler"/> class.
/// </summary>
public class ListProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ListProductHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListProductHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public ListProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new ListProductHandler(_productRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid ListProductCommand returns a paginated product list.
    /// </summary>
    [Fact(DisplayName = "Given valid command When listing products Then returns paginated result")]
    public async Task Handle_ValidRequest_ReturnsPaginatedProducts()
    {
        // Given
        var command = ListProductHandlerTestData.GenerateValidCommand();
        var products = ListProductHandlerTestData.GenerateProductList();
        var mappedProducts = ListProductHandlerTestData.GenerateMappedProductList();

        var queryableProducts = products.AsQueryable().BuildMock(); // Using MockQueryable

        _productRepository.Query(Arg.Any<CancellationToken>()).Returns(queryableProducts);
        _mapper.Map<IEnumerable<ProductResult>>(Arg.Any<IEnumerable<Product>>()).Returns(mappedProducts);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Data.Should().NotBeEmpty();
        result.TotalCount.Should().Be(products.Count);
        result.CurrentPage.Should().Be(command.Page ?? 1);
        result.TotalPages.Should().Be((int)Math.Ceiling(products.Count / (double)(command.Size ?? 10)));

        await _productRepository.Received(1).Query(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid ListProductCommand throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid command When listing products Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = ListProductHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    /// <summary>
    /// Tests that an empty product list returns an empty result.
    /// </summary>
    [Fact(DisplayName = "Given no products in repository When listing products Then returns empty result")]
    public async Task Handle_NoProducts_ReturnsEmptyResult()
    {
        // Given
        var command = ListProductHandlerTestData.GenerateValidCommand();
        var emptyQuery = Enumerable.Empty<Product>().AsQueryable().BuildMock();

        _productRepository.Query(Arg.Any<CancellationToken>()).Returns(emptyQuery);

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