using Estimate.Domain.Entities.Base;

namespace Estimate.Domain.Entities;

public class EstimateEn : Entity
{
    public string Name { get; private set; }

    public Guid SupplierId { get; private set; }
    public Supplier Supplier { get; }

    public List<ProductInEstimate> ProductsInEstimate { get; private set; } = new();

    public EstimateEn(
        Guid id,
        string name,
        Guid supplierId)
    {
        Id = id;
        Name = name;
        SupplierId = supplierId;
    }

    public void AlterName(string name) =>
        Name = name;

    public void AlterSupplier(Guid supplierId) =>
        SupplierId = supplierId;

    public void UpdateProducts(List<ProductInEstimate> products) =>
        ProductsInEstimate = products;
}