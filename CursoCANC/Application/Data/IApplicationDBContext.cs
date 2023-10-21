using Domain.Customers;
using Domain.Employes;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDBContext
{
    DbSet<Customer> Customers { get; set; }
    DbSet<Employe> Employes { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}