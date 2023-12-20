namespace Estimate.Application.Products.CreateProductUseCase;

public class CreateProductCommand
{
    public string Name { get; set; }

    public CreateProductCommand(
        string name)
    {
        Name = name;
    }
}