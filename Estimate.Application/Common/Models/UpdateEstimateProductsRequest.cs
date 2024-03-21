namespace Estimate.Application.Estimates.UpdateEstimateProductsUseCase;

public record UpdateEstimateProductsRequest(
    Guid ProductId,
    double Quantity,
    decimal UnitPrice);
