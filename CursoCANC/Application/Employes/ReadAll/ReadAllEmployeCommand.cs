using Application.Employes.Common;
using ErrorOr;
using MediatR;

namespace Application.Employes.ReadAll
{
    public record ReadAllEmployeCommand() : IRequest<ErrorOr<IReadOnlyList<EmployeResponse>>>;
}
