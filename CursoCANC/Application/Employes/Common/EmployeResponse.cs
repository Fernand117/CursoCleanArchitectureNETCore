namespace Application.Employes.Common
{
    public record EmployeResponse(
        Guid Id,
        string Nombre,
        string Paterno,
        string Materno,
        DateTime FechaNacimiento,
        bool Active
    );
}
