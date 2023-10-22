using Application.Employes.Create;
using Application.Employes.Delete;
using Application.Employes.ReadAll;
using Application.Employes.ReadById;
using Application.Employes.Update;
using CCANC.API.Common;
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

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetAll()
    {
        ApiResponse apiResponse = new ApiResponse();
        var employersResult = await _mediator.Send(new ReadAllEmployeCommand());

        try
        {
            apiResponse.ResponseCode = 200;
            apiResponse.ResponseText = "Transacción completada";
            apiResponse.Data = employersResult.Value;
        }
        catch (Exception e)
        {
            apiResponse.ResponseCode = 404;
            apiResponse.ResponseText = "Error en la transacción";
            apiResponse.Data = employersResult.Errors;
        }

        return apiResponse;
    }

    [HttpGet("id/{Id}")]
    public async Task<IActionResult> GetById(Guid Id)
    {
        var employerResult = await _mediator.Send(new ReadByIdEmployeCommand(Id));

        return employerResult.Match(
            employer => Ok(employer),
            error => Problem(error)
        );
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

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid Id)
    {
        var deleteResult = await _mediator.Send(new DeleteEmployeCommand(Id));

        return deleteResult.Match(
            employeId => NoContent(),
            errors => Problem(errors)
        );
    }
}