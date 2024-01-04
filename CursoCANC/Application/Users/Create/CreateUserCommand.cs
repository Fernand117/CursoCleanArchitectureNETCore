using ErrorOr;
using MediatR;

namespace Application.Users.Create;

public record CreateUserCommand(
    string Username,
    string Email,
    string Password
) : IRequest<ErrorOr<Unit>>;