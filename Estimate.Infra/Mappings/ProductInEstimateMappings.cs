using Estimate.Domain.Entities;
using Estimate.Infra.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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