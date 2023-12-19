﻿using MediatR;

namespace Estimate.Application.Estimates.UpdateEstimateProductsUseCase;

public class UpdateEstimateProductsCommand : IRequest<UpdateEstimateProductsResult>
{
    public Guid EstimateId { get; set; }
    public List<UpdateEstimateProductsRequest> UpdateEstimateProducts { get; set; } = new ();
}