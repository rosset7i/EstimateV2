using Estimate.Domain.Common;
using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using MediatR;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Products.RemoveProductUseCase;

public class RemoveProductHandler : IRequestHandler<RemoveProductCommand, ResultOf<Operation>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveProductHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultOf<Operation>> Handle(RemoveProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FetchByIdAsync(command.ProductId);

        if (product is null)
            return DomainError.Common.NotFound<Product>();

        _productRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync();

        return Operation.Deleted;
    }
}