using Domain.Employes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class EmployeRepository : IEmployeRepository
{
    private readonly ApplicationDBContext _context;

    public EmployeRepository(ApplicationDBContext context)
    {
        _context = context ?? throw new ArgumentException(nameof(context));
    }

    public async Task<List<Employe>> GetAll() => await _context.Employes.Where(e => e.Active == true).ToListAsync();

    public async Task<Employe?> GetByIdAsync(EmployeId id) =>
        await _context.Employes.SingleOrDefaultAsync(e => e.Id == id);

    public async Task<bool> ExistAsync(EmployeId id) => await _context.Employes.AnyAsync(e => e.Active == true);

    public void Add(Employe employe) => _context.Employes.Add(employe);

    public void Update(Employe employe) => _context.Employes.Update(employe);

    public void Delete(Employe employe) => _context.Employes.Remove(employe);
}