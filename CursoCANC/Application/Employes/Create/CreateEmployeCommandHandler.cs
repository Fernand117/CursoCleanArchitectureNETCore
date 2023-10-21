using Domain.Employes;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Employes.Create
{
    internal sealed class CreateEmployeCommandHandler : IRequestHandler<CreateEmployeCommand, ErrorOr<Unit>>
    {
        private readonly IEmployeRepository _employeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeCommandHandler(IEmployeRepository employeRepository, IUnitOfWork unitOfWork)
        {
            _employeRepository = employeRepository ?? throw new ArgumentException(nameof(employeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateEmployeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employe = new Employe(
                    new EmployeId(Guid.NewGuid()),
                    request.Nombre,
                    request.Paterno,
                    request.Materno,
                    request.FechaNacimiento,
                    true
                );

                _employeRepository.Add(employe);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception e)
            {
                return Error.Failure("CreateEmploye.Failure", e.Message);
            }
        }
    }
}