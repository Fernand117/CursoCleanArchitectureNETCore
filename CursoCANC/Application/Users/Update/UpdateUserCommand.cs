using ErrorOr;
using MediatR;

namespace Application.Users.Update;

public record UpdateUserCommand(
    Guid Id,
    string Username,
    string Email,
    string Password
) : IRequest<ErrorOr<Unit>>;