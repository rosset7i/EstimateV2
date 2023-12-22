using Estimate.Domain.Entities.Base;
using Estimate.Domain.Entities.Estimate;

namespace Estimate.Domain.Entities;

public class Supplier : Entity
{
    public string Name { get; private set; }

    public List<EstimateEn> Estimates { get; } = new();

    public Supplier(
        Guid id,
        string name)
    {
        Id = id;
        Name = name;
    }

    public void AlterName(string name) =>
        Name = name;
}