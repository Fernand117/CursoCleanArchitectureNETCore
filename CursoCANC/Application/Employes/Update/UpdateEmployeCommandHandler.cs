using Domain.Employes;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Employes.Update
{
    internal sealed class UpdateEmployeCommandHandler : IRequestHandler<UpdateEmployeCommand, ErrorOr<Unit>>
    {
        private readonly IEmployeRepository _employeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeCommandHandler(IEmployeRepository employeRepository, IUnitOfWork unitOfWork)
        {
            _employeRepository = employeRepository ?? throw new ArgumentException(nameof(employeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateEmployeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _employeRepository.ExistAsync(new EmployeId(request.Id)))
                {
                    return Error.NotFound("Employe.NotFound", "The employe with the provide ID was not found.");
                }

                Employe employe = Employe.UpdateEmploye(request.Id, request.Nombre, request.Paterno, request.Materno, request.FechaNacimiento, true);
            
                _employeRepository.Update(employe);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            
                return Unit.Value;
            }
            catch (Exception e)
            {
                return Error.Failure("UpdateEmploye.Failure", e.Message);
            }
        }
    }
}