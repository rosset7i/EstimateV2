using Estimate.Application.Common.Repositories;
using Estimate.Domain.Entities;
using MediatR;
using Rossetti.Common.Data.Repository;
using Rossetti.Common.ErrorHandler;
using Rossetti.Common.Result;

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

        product.AlterName(command.Name);

        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Operation.Updated;
    }
}