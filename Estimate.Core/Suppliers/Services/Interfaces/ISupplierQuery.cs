using Estimate.Core.Suppliers.Dtos;
using Estimate.Infra.Repositories.Base.Models.PagingAndSorting;

namespace Estimate.Core.Suppliers.Services.Interfaces;

public interface ISupplierQuery
{
    Task<PagedResultOf<SupplierResponse>> FetchPagedSuppliersAsync(PagedAndSortedSupplierRequest input);
}