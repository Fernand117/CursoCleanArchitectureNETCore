using Application.Customers.Common;
using Domain.Customers;
using ErrorOr;
using MediatR;

namespace Application.Customers.ReadAll;

internal sealed class ReadAllCustomerCommandHandler : IRequestHandler<ReadAllCustomerCommand, ErrorOr<IReadOnlyList<CustomerResponse>>>
{
    private readonly ICustomerRepository _customerRepository;

    public ReadAllCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentException(nameof(customerRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<CustomerResponse>>> Handle(ReadAllCustomerCommand request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Customer> customers = await _customerRepository.GetAll();

        return customers.Select(customer => new CustomerResponse(
            customer.Id.Value,
            customer.FullName,
            customer.Email,
            customer.PhoneNumber.Value,
            new AddressResponse(customer.Address.Country,
                customer.Address.Line1,
                customer.Address.Line2,
                customer.Address.City,
                customer.Address.State,
                customer.Address.ZipCode),
            customer.Active)).ToList();
    }
}