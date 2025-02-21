using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Products;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="UpdateProductHandler"/> class.
/// </summary>
public class UpdateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IUser _user;
    private readonly UpdateProductHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProductHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public UpdateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _user = Substitute.For<IUser>();

        _handler = new UpdateProductHandler(_productRepository, _mapper, _user);
    }

    /// <summary>
    /// Tests that a valid UpdateProductCommand is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid update command When updating product Then returns updated product details")]
    public async Task Handle_ValidRequest_ReturnsUpdatedProductDetails()
    {
        // Given
        var command = UpdateProductHandlerTestData.GenerateValidCommand();
        var existingProduct = UpdateProductHandlerTestData.GenerateExistingProduct();
        var updatedProduct = UpdateProductHandlerTestData.GenerateUpdatedProduct();
        var expectedResult = UpdateProductHandlerTestData.GenerateValidResult();

        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingProduct);
        _mapper.Map(command, existingProduct).Returns(updatedProduct);
        _user.Username.Returns("admin");
        _productRepository.UpdateAsync(existingProduct, Arg.Any<CancellationToken>()).Returns(updatedProduct);
        _mapper.Map<UpdateProductResult>(updatedProduct).Returns(expectedResult);

        // When
        var validationResponse = command.Validate();
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(expectedResult.Id);
        result.Title.Should().Be(expectedResult.Title);
        result.Category.Should().Be(expectedResult.Category);

        Assert.True(validationResponse.IsValid);
        Assert.Empty(validationResponse.Errors);
        await _productRepository.Received(1).UpdateAsync(existingProduct, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid UpdateProductCommand throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid update command When updating product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = UpdateProductHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that updating a non-existing product throws an InvalidOperationException.
    /// </summary>
    [Fact(DisplayName = "Given non-existing product ID When updating product Then throws invalid operation exception")]
    public async Task Handle_NonExistingProduct_ThrowsInvalidOperationException()
    {
        // Given
        var command = UpdateProductHandlerTestData.GenerateValidCommand();

        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).ReturnsNull();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Entity with id {command.Id} not found");
    }
}