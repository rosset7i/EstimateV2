using Estimate.Application.Suppliers.Dtos;
using Estimate.Infra.Repositories.Base.Models.PagingAndSorting;

namespace Estimate.Application.Suppliers.Services.Interfaces;

public interface ISupplierQuery
{
    Task<PagedResultOf<SupplierResponse>> FetchPagedSuppliersAsync(PagedAndSortedSupplierRequest input);
}