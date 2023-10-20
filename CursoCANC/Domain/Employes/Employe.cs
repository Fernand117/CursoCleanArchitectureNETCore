using Domain.Primitives;

namespace Domain.Employes;

public class Employe : AgregateRoot
{
    public Employe(){}

    public Employe(EmployeId id, string nombre, string paterno, string materno, DateTime fechaNacimiento, bool active)
    {
        Id = id;
        Nombre = nombre;
        Paterno = paterno;
        Materno = materno;
        FechaNacimiento = fechaNacimiento;
        Active = active;
    }

    public EmployeId Id { get; private set; }
    public string Nombre { get; private set; }
    public string Paterno { get; private set; }
    public string Materno { get; private set; }
    public DateTime FechaNacimiento { get; private set; }
    public bool Active { get; private set; }
    
    public Employe UpdateEmploye(Guid id, string nombre, string paterno, string materno, DateTime fechaNacimiento, bool active)
    {
        return new Employe(new EmployeId(id), nombre, paterno, materno, fechaNacimiento, active);
    }
}