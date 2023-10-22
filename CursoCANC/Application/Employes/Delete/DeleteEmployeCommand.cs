using ErrorOr;
using MediatR;

namespace Application.Employes.Delete;

public record class DeleteEmployeCommand(Guid Id) : IRequest<ErrorOr<Unit>>;