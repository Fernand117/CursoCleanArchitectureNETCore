using ErrorOr;
using MediatR;

namespace Application.Employes.Update
{
    public record UpdateEmployeCommand(
        Guid Id,
        string Nombre,
        string Paterno,
        string Materno,
        DateTime FechaNacimiento
    ) : IRequest<ErrorOr<Unit>>;
}
