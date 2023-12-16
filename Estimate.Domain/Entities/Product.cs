using Estimate.Domain.Entities.Base;

namespace Estimate.Domain.Entities;

public class Product : Entity
{
    public string Name { get; private set; }

    public List<ProductInEstimate> ProductsInEstimate { get; } = new();

    public Product(
        Guid id, 
        string name)
    {
        Id = id;
        Name = name;
    }

    public void AlterName(string name) =>
        Name = name;
}