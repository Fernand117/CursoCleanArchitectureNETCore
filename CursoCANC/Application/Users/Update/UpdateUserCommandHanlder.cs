using Domain.Primitives;
using Domain.Users;
using ErrorOr;
using MediatR;

namespace Application.Users.Update;

public sealed class UpdateUserCommandHanlder : IRequestHandler<UpdateUserCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHanlder(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
    }
    
    public async Task<ErrorOr<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!await _userRepository.ExistAsync(new UserId(request.Id)))
            {
                return Error.NotFound("User.NotFound", "The userId not found.");
            }

            User user = User.UpdateUser(request.Id, request.Username, request.Email, request.Password);
            
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        catch (Exception e)
        {
            return Error.Failure("UpdateUser.Failure", e.Message);
        }
    }
}