using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using MediatR;

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

        await _productRepository.AddAsync(newProduct);
        await _unitOfWork.SaveChangesAsync();

        return Operation.Created;
    }
}