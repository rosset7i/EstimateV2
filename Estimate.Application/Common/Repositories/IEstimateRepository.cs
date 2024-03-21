using Estimate.Application.Common.Repositories.Base;
using Estimate.Domain.Entities.Estimate;

namespace Estimate.Application.Common.Repositories;

public interface IEstimateRepository : IRepositoryBase<Guid, EstimateEn>
{
    Task<EstimateEn> FetchEstimateWithProducts(Guid estimateId);
    Task UpdateProducts(EstimateEn estimate);
}