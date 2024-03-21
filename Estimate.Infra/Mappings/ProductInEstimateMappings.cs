using Estimate.Domain.Entities.Estimate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossetti.Common.Data;

namespace Estimate.Infra.Mappings;

public class ProductInEstimateMappings : IEntityTypeConfiguration<ProductInEstimate>
{
    public void Configure(EntityTypeBuilder<ProductInEstimate> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .ComplexProperty(e => e.Price)
            .IsValueObject();

        builder.ToTable("ProductInEstimate");
    }
}