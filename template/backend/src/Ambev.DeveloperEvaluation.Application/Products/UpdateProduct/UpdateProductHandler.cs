using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Handler for processing UpdateProductCommand requests
/// </summary>
public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    /// <summary>
    /// Initializes a new instance of UpdateProductHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="user">The Authenticated User</param>
    public UpdateProductHandler(IProductRepository productRepository, IMapper mapper, IUser user)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _user = user;
    }

    /// <summary>
    /// Handles the UpdateProductCommand request
    /// </summary>
    /// <param name="command">The UpdateProductCommand command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product details</returns>
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var entity = await _productRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Entity with id {command.Id} not found");

        _mapper.Map(command, entity);

        entity.UpdatedBy = _user.Username;
        entity.UpdatedAt = DateTime.UtcNow;

        var updatedProduct = await _productRepository.UpdateAsync(entity, cancellationToken);

        var result = _mapper.Map<UpdateProductResult>(updatedProduct);

        return result;
    }
}