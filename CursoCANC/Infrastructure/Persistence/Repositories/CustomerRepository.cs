using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDBContext _context;

    public CustomerRepository(ApplicationDBContext context)
    {
        _context = context ?? throw new AggregateException(nameof(context));
    }

    public void Add(Customer customer) => _context.Customers.Add(customer);
    public void Update(Customer customer) => _context.Customers.Update(customer);
    public void Delete(Customer customer) => _context.Customers.Remove(customer);

    public async Task<List<Customer>> GetAll() => await _context.Customers.Where(c => c.Active == true).ToListAsync();
    public async Task<Customer?> GetByIdAsync(CustomerId id) => await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
    public async Task<bool> ExistAsync(CustomerId id) => await _context.Customers.AnyAsync(c => c.Active == true);
}