namespace Estimate.Application.Products.CreateProductUseCase;

public class CreateProductRequest
{
    public string Name { get; set; }

    public CreateProductRequest(
        string name)
    {
        Name = name;
    }
}