using Domain.Employes;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Employes.Delete;

internal class DeleteEmployeCommandHandler : IRequestHandler<DeleteEmployeCommand, ErrorOr<Unit>>
{
    private readonly IEmployeRepository _employeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEmployeCommandHandler(IEmployeRepository employeRepository, IUnitOfWork unitOfWork)
    {
        _employeRepository = employeRepository ?? throw new ArgumentException(nameof(employeRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteEmployeCommand request, CancellationToken cancellationToken)
    {
        if (await _employeRepository.GetByIdAsync(new EmployeId(request.Id)) is not Employe employe)
        {
            return Error.NotFound("Employe.NotFound", "The employe provide ID was not found.");
        }
        
        _employeRepository.Delete(employe);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}