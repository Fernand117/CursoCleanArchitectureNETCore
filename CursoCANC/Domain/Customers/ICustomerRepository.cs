namespace Domain.Customers;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid Value);
    Task Add(Customer customer);
}