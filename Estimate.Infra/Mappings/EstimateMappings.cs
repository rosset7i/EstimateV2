using Estimate.Domain.Entities.Estimate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estimate.Infra.Mappings;

public class EstimateMappings : IEntityTypeConfiguration<EstimateEn>
{
    public void Configure(EntityTypeBuilder<EstimateEn> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(75);

        builder
            .HasOne(e => e.Supplier)
            .WithMany(e => e.Estimates)
            .HasForeignKey(e => e.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(e => e.ProductsInEstimate)
            .WithOne(e => e.Estimate)
            .HasForeignKey(e => e.EstimateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Estimate");
    }
}