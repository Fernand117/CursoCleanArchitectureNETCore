using Application.Customers.Common;
using Domain.Customers;
using ErrorOr;
using MediatR;

namespace Application.Customers.ReadById;

internal sealed class ReadByIdCommandHandler : IRequestHandler<ReadByIdCommand, ErrorOr<CustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository;

    public ReadByIdCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentException(nameof(customerRepository));
    }

    public async Task<ErrorOr<CustomerResponse>> Handle(ReadByIdCommand request, CancellationToken cancellationToken)
    {
        if (await _customerRepository.GetByIdAsync(new CustomerId(request.Id)) is not Customer customer)
        {
            return Error.NotFound("Customer.NotFound", "The customer with the provide ID was not found");
        }

        return new CustomerResponse(
            customer.Id.Value,
            customer.FullName,
            customer.Email,
            customer.PhoneNumber.Value,
            new AddressResponse(
                customer.Address.Country,
                customer.Address.Line1,
                customer.Address.Line2,
                customer.Address.City,
                customer.Address.State,
                customer.Address.ZipCode),
            customer.Active);
    }
}