using Application.Users.Common;
using Domain.Users;
using ErrorOr;
using MediatR;

namespace Application.Users.ReadAll
{
    internal sealed class ReadAllUserCommandHandler : IRequestHandler<ReadAllUserCommand, ErrorOr<IReadOnlyList<UserResponse>>>
    {
        private readonly IUserRepository _userRepository;

        public ReadAllUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<UserResponse>>> Handle(ReadAllUserCommand request, CancellationToken cancellationToken)
        {
            IReadOnlyList<User> users = await _userRepository.GetAll();

            return users.Select(user => new UserResponse(
                user.Id.Id,
                user.UserName,
                user.Email,
                user.Password,
                user.Active
            )).ToList();
        }
    }
}
