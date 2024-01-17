using Application.Users.Common;
using ErrorOr;
using MediatR;

namespace Application.Users.ReadAll
{
    public record ReadAllUserCommand() : IRequest<ErrorOr<IReadOnlyList<UserResponse>>>;
}
