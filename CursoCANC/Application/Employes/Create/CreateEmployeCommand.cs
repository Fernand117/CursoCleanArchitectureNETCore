using ErrorOr;
using MediatR;

namespace Application.Employes.Create;

public record CreateEmployeCommand(
    string Nombre,
    string Paterno,
    string Materno,
    DateTime FechaNacimiento
) : IRequest<ErrorOr<Unit>>;