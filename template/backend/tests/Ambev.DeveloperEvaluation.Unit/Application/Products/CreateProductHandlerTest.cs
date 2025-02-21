using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Products;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="CreateProductHandler"/> class.
/// </summary>
public class CreateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IUser _user;
    private readonly CreateProductHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _user = Substitute.For<IUser>();
        _handler = new CreateProductHandler(_productRepository, _mapper, _user);
    }

    /// <summary>
    /// Tests that a valid product creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid product data When creating product Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            CreatedBy = "testUser"
        };

        var result = new CreateProductResult
        {
            Id = product.Id,
        };

        _mapper.Map<Product>(command).Returns(product);
        _mapper.Map<CreateProductResult>(product).Returns(result);

        _productRepository.GetByTitleAsync(command.Title, Arg.Any<CancellationToken>())
            .Returns((Product?)null);
        _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(product);

        _user.Username.Returns("testUser");

        // When
        var validationResponse = command.Validate();
        var createProductResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createProductResult.Should().NotBeNull();
        createProductResult.Id.Should().Be(product.Id);
        Assert.True(validationResponse.IsValid);
        Assert.Empty(validationResponse.Errors);
        await _productRepository.Received(1).CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid product creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid product data When creating product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateProductCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that an attempt to create a product with an existing title throws an exception.
    /// </summary>
    [Fact(DisplayName = "Given existing product title When creating product Then throws invalid operation exception")]
    public async Task Handle_ExistingTitle_ThrowsInvalidOperationException()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        var existingProduct = new Product { Title = command.Title, CreatedBy = "testUser" };

        _productRepository.GetByTitleAsync(command.Title, Arg.Any<CancellationToken>())
            .Returns(existingProduct);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Product with title {command.Title} already exists");
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to product entity")]
    public async Task Handle_ValidRequest_MapsCommandToProduct()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            CreatedBy = "testUser"
        };

        _mapper.Map<Product>(command).Returns(product);
        _productRepository.GetByTitleAsync(command.Title, Arg.Any<CancellationToken>())
            .Returns((Product?)null);
        _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(product);
        _user.Username.Returns("testUser");

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Product>(Arg.Is<CreateProductCommand>(c =>
            c.Title == command.Title));
    }
}