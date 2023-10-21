using Application.Employes.Create;
using Application.Employes.Update;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CCANC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeController : ApiController
{
    private readonly ISender _mediator;

    public EmployeController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeCommand command)
    {
        var createEmployeResult = await _mediator.Send(command);

        return createEmployeResult.Match(employe => Ok(), errors => Problem(errors));
    }

    [HttpPut("id/{Id}")]
    public async Task<IActionResult> Update(Guid Id, [FromBody] UpdateEmployeCommand command)
    {
        if (command.Id != Id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Employe.UpdateInfo", "The request ID does not match with the url ID.")
            };

            return Problem();
        }

        var updateResult = await _mediator.Send(command);

        return updateResult.Match(employeId => NoContent(), errors => Problem(errors));
    }
}