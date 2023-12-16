using Estimate.Domain.Entities;
using Estimate.Domain.Interface.Base;

namespace Estimate.Domain.Interface;

public interface IEstimateRepository : IRepositoryBase<Guid, EstimateEn>
{
    Task<EstimateEn?> FetchEstimateWithProducts(Guid estimateId);
    Task UpdateProducts(EstimateEn estimate);
}