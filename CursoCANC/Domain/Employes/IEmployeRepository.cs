namespace Domain.Employes;

public interface IEmployeRepository
{
    Task<List<Employe>> GetAll();
    Task<Employe?> GetByIdAsync(EmployeId id);
    Task<bool> ExistAsync(EmployeId id);
    void Add(Employe employe);
    void Update(Employe employe);
    void Delete(Employe employe);
}