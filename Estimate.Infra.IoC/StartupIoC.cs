using Estimate.Application.Common;
using Estimate.Application.Common.Repositories;
using Estimate.Infra.AppDbContext;
using Estimate.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Estimate.Infra.IoC;

public static class StartupIoC
{
    public static void AddContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<EstimateDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDatabaseContext, EstimateDbContext>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IEstimateRepository, EstimateRepository>();
    }
}
