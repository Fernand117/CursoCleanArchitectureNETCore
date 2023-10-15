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

    public async Task Add(Customer customer) => await _context.Customers.AddAsync(customer);

    public async Task<Customer?> GetByIdAsync(CustomerId id) => await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
}