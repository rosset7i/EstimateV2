using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using MediatR;
using Rossetti.Common.Result;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Products.UpdateProductUseCase;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ResultOf<Operation>>
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

    public async Task<ResultOf<Operation>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FetchByIdAsync(command.ProductId);

        if (product is null)
            return CommonError.NotFound<Product>();

        var updatedProduct = command.UpdateInfoOf(product);

        _productRepository.Update(updatedProduct);
        await _unitOfWork.SaveChangesAsync();

        return Operation.Updated;
    }
}