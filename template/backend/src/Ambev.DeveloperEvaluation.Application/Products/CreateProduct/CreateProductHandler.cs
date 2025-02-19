using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Handler for processing CreateProductCommand requests
/// </summary>
public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    /// <summary>
    /// Initializes a new instance of CreateProductHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="user">The Authenticated User</param>
    public CreateProductHandler(IProductRepository productRepository, IMapper mapper, IUser user)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _user = user;
    }

    /// <summary>
    /// Handles the CreateProductCommand request
    /// </summary>
    /// <param name="command">The command to be processed</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Product Id</returns>
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingProduct = await _productRepository.GetByTitleAsync(command.Title, cancellationToken);

        if (existingProduct is not null)
            throw new InvalidOperationException($"Product with title {command.Title} already exists");

        var product = _mapper.Map<Product>(command);

        product.CreatedBy = _user.Username;

        var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);
        var result = _mapper.Map<CreateProductResult>(createdProduct);

        return result;
    }
}
