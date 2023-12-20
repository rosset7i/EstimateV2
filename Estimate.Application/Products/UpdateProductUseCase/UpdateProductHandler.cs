using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using MediatR;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Products.UpdateProductUseCase;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FetchByIdAsync(command.ProductId);

        if (product is null)
            throw new BusinessException(DomainError.Common.NotFound<Product>());

        var updatedProduct = command.UpdateInfoOf(product);

        _productRepository.Update(updatedProduct);
        await _unitOfWork.SaveChangesAsync();

        return new UpdateProductResult();
    }
}