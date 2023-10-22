using Application.Employes.Common;
using ErrorOr;
using MediatR;

namespace Application.Employes.ReadById;

public record class ReadByIdEmployeCommand(Guid Id) : IRequest<ErrorOr<EmployeResponse>>;