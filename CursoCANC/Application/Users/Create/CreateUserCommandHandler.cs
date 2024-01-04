using Domain.Primitives;
using Domain.Users;
using ErrorOr;
using MediatR;

namespace Application.Users.Create;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
    }
    
    public async Task<ErrorOr<Unit>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = new User(
                new UserId(Guid.NewGuid()),
                request.Username,
                request.Email,
                request.Password
            );
            
            _userRepository.Add(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        catch (Exception e)
        {
            return Error.Failure("CreateUser.Failure", e.Message);
        }
    }
}