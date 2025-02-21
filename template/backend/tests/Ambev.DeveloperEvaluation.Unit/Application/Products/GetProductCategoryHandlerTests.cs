using Ambev.DeveloperEvaluation.Application.Products.GetProductCategory;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Products;
using FluentAssertions;
using MockQueryable;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="GetProductCategoryHandler"/> class.
/// </summary>
public class GetProductCategoryHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly GetProductCategoryHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductCategoryHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public GetProductCategoryHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new GetProductCategoryHandler(_productRepository);
    }

    /// <summary>
    /// Tests that when the product repository returns products, the categories are retrieved correctly.
    /// </summary>
    [Fact(DisplayName = "Given products exist When getting product categories Then returns categories list")]
    public async Task Handle_ProductsExist_ReturnsCategoriesList()
    {
        // Given
        var command = new GetProductCategoryCommand();
        var products = GetProductCategoryHandlerTestData.GenerateValidProducts();
        var queryableProducts = products.AsQueryable().BuildMock(); // Using MockQueryable

        _productRepository.Query(Arg.Any<CancellationToken>()).Returns(queryableProducts);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Categories.Should().HaveCount(1);
        await _productRepository.Received(1).Query(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that the product repository is called to query for products.
    /// </summary>
    [Fact(DisplayName = "Given valid command When getting product categories Then calls repository query")]
    public async Task Handle_ValidCommand_CallsRepositoryQuery()
    {
        // Given
        var command = new GetProductCategoryCommand();
        var products = GetProductCategoryHandlerTestData.GenerateValidProducts();
        var queryableProducts = products.AsQueryable().BuildMock(); // Using MockQueryable

        _productRepository.Query(Arg.Any<CancellationToken>()).Returns(queryableProducts);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _productRepository.Received(1).Query(Arg.Any<CancellationToken>());
    }
}