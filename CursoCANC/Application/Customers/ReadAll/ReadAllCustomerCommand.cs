using Application.Customers.Common;
using ErrorOr;
using MediatR;

namespace Application.Customers.ReadAll;

public record ReadAllCustomerCommand() : IRequest<ErrorOr<IReadOnlyList<CustomerResponse>>>;