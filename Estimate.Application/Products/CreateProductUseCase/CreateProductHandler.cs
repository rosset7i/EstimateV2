using Estimate.Application.Common.Repositories;
using Estimate.Domain.Entities;
using MediatR;
using Rossetti.Common.Data.Repository;
using Rossetti.Common.Result;

namespace Estimate.Application.Products.CreateProductUseCase;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ResultOf<Operation>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultOf<Operation>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var newProduct = new Product(
            Guid.NewGuid(),
            command.Name);

        await _productRepository.AddAsync(newProduct, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Operation.Created;
    }
}