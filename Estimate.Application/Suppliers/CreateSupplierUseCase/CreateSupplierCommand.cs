using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Suppliers.CreateSupplierUseCase;

public record CreateSupplierCommand(string Name) : IRequest<ResultOf<Operation>>;