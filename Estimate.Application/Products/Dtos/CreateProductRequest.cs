namespace Estimate.Application.Products.Dtos;

public class CreateProductRequest
{
    public string Name { get; set; }

    public CreateProductRequest(
        string name)
    {
        Name = name;
    }
}