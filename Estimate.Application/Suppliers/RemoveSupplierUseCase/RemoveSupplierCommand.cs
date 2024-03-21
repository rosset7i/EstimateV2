using Estimate.Domain.Common.CommonResults;
using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Suppliers.RemoveSupplierUseCase;

public record RemoveSupplierCommand(Guid SupplierId) : IRequest<ResultOf<Operation>>;