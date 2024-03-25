using Estimate.Domain.Entities.Estimate;
using Rossetti.Common.Data.Repository;

namespace Estimate.Application.Common.Repositories;

public interface IEstimateRepository : IRepositoryBase<Guid, EstimateEn>
{
    Task<EstimateEn> FetchEstimateWithProducts(Guid estimateId);
    Task UpdateProducts(EstimateEn estimate);
}