using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Suppliers.UpdateSupplierUseCase;

public record UpdateSupplierCommand(
    Guid SupplierId,
    string Name) : IRequest<ResultOf<Operation>>;
