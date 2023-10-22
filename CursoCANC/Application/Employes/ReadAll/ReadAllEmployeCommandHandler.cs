using Application.Employes.Common;
using Domain.Employes;
using ErrorOr;
using MediatR;

namespace Application.Employes.ReadAll
{
    internal sealed class ReadAllEmployeCommandHandler : IRequestHandler<ReadAllEmployeCommand, ErrorOr<IReadOnlyList<EmployeResponse>>>
    {
        private readonly IEmployeRepository _employeRepository;

        public ReadAllEmployeCommandHandler(IEmployeRepository employeRepository)
        {
            _employeRepository = employeRepository ?? throw new ArgumentException(nameof(employeRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<EmployeResponse>>> Handle(ReadAllEmployeCommand request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Employe> employes = await _employeRepository.GetAll();

            return employes.Select(employe => new EmployeResponse(
                employe.Id.Id,
                employe.Nombre,
                employe.Paterno,
                employe.Materno,
                employe.FechaNacimiento,
                employe.Active
            )).ToList();
        }
    }
}
