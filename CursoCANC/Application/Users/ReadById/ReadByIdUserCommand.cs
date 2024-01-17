using Application.Users.Common;
using ErrorOr;
using MediatR;

namespace Application.Users.ReadById
{
    public record class ReadByIdUserCommand(Guid Id) : IRequest<ErrorOr<UserResponse>>;
}
