using Application.Customers.Common;
using ErrorOr;
using MediatR;

namespace Application.Customers.ReadById;

public record ReadByIdCommand(Guid Id) : IRequest<ErrorOr<CustomerResponse>>;