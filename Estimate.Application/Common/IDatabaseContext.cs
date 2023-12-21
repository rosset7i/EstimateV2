using Estimate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Estimate.Application.Infrastructure;

public interface IDatabaseContext
{
    DbSet<EstimateEn> Estimate { get; set; }
    DbSet<Product> Product { get; set; }
    DbSet<Supplier> Supplier { get; set; }
    DbSet<ProductInEstimate> ProductInEstimate { get; set; }
    DbSet<User> User { get; set; }
}