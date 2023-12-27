using Estimate.Application.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Entities.Estimate;
using Estimate.Infra.Mappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Estimate.Infra.AppDbContext;

public class EstimateDbContext : IdentityDbContext, IDatabaseContext
{
    public DbSet<EstimateEn> Estimate { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Supplier> Supplier { get; set; }
    public DbSet<ProductInEstimate> ProductInEstimate { get; set; }
    public DbSet<User> User { get; set; }

    public EstimateDbContext(DbContextOptions option) : base(option)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        new SupplierMappings().Configure(modelBuilder.Entity<Supplier>());
        new EstimateMappings().Configure(modelBuilder.Entity<EstimateEn>());
        new ProductMappings().Configure(modelBuilder.Entity<Product>());
        new ProductInEstimateMappings().Configure(modelBuilder.Entity<ProductInEstimate>());
    }
}