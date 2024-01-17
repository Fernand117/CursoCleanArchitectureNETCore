using Application.Users.Common;
using Domain.Users;
using ErrorOr;
using MediatR;

namespace Application.Users.ReadById
{
    internal sealed class ReadByIdUserCommandHandler : IRequestHandler<ReadByIdUserCommand, ErrorOr<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public ReadByIdUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
        }

        public async Task<ErrorOr<UserResponse>> Handle(ReadByIdUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByIdAsync(new UserId(request.Id)) is not User user)
            {
                return Error.NotFound("User.NotFound", "The user with the provide id was not found.");
            }

            return new UserResponse(
                user.Id.Id,
                user.UserName,
                user.Email,
                user.Password,
                user.Active);
        }
    }
}
