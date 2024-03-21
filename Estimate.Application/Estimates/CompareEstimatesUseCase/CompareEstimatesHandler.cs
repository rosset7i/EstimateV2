using Estimate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rossetti.Common.Result;

namespace Estimate.Application.Estimates.CompareEstimatesUseCase;

public class CompareEstimatesHandler : IRequestHandler<CompareEstimatesQuery, ResultOf<List<CompareEstimatesResponse>>>
{
    private readonly IDatabaseContext _dbContext;

    public CompareEstimatesHandler(IDatabaseContext dbContext) =>
        _dbContext = dbContext;

    public async Task<ResultOf<List<CompareEstimatesResponse>>> Handle(CompareEstimatesQuery query, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Estimate
            .Where(e => query.EstimateIds.Contains(e.Id))
            .Include(e => e.Supplier)
            .Include(e => e.ProductsInEstimate)
                .ThenInclude(p => p.Product)
            .Select(e => CompareEstimatesResponse.Of(e))
            .ToListAsync(cancellationToken: cancellationToken);

        return MapEconomicChoices(result);
    }

    private static List<CompareEstimatesResponse> MapEconomicChoices(List<CompareEstimatesResponse> estimates)
    {
        var products = estimates
            .SelectMany(e => e.ProductsInEstimate)
            .GroupBy(e => e.Id);

        foreach (var product in products)
        {
            var productsGroup = product.ToList();

            if (!productsGroup.Any())
                continue;

            var lowestPricedProduct = productsGroup.MinBy(e => e.TotalPrice)!;
            lowestPricedProduct.IsEconomic = true;
        }

        return estimates;
    }
}