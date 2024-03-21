namespace Estimate.Application.Common.Models;

public record UpdateEstimateProductsRequest(
    Guid ProductId,
    double Quantity,
    decimal UnitPrice);
