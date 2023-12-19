using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;

namespace Estimate.Application.Products.CreateProductUseCase;

public class CreateProductHandler
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

    public async Task CreateProductAsync(CreateProductRequest request)
    {
        var newProduct = new Product(
            Guid.NewGuid(),
            request.Name);

        await _productRepository.AddAsync(newProduct);
        await _unitOfWork.SaveChangesAsync();
    }
}