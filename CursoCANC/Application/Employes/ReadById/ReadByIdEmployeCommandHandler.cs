using Application.Employes.Common;
using Domain.Employes;
using ErrorOr;
using MediatR;

namespace Application.Employes.ReadById;

internal sealed class ReadByIdEmployeCommandHandler : IRequestHandler<ReadByIdEmployeCommand, ErrorOr<EmployeResponse>>
{
    private readonly IEmployeRepository _employeRepository;

    public ReadByIdEmployeCommandHandler(IEmployeRepository employeRepository)
    {
        _employeRepository = employeRepository ?? throw new ArgumentException(nameof(employeRepository));
    }

    public async Task<ErrorOr<EmployeResponse>> Handle(ReadByIdEmployeCommand request, CancellationToken cancellationToken)
    {
        if (await _employeRepository.GetByIdAsync(new EmployeId(request.Id)) is not Employe employe)
        {
            return Error.NotFound("Employe.NotFound", "The employe with the provide ID was not found");
        }

        return new EmployeResponse(
            employe.Id.Id,
            employe.Nombre,
            employe.Paterno,
            employe.Materno,
            employe.FechaNacimiento,
            employe.Active);
    }
}