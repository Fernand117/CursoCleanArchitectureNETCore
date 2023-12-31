using Application.Employes.Create;
using Application.Employes.Delete;
using Application.Employes.ReadAll;
using Application.Employes.ReadById;
using Application.Employes.Update;
using CCANC.API.Common;
using CCANC.API.Common.ENUMS;
using CCANC.API.Common.Resources;
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
        
        if (employersResult.IsError)
        {
            apiResponse.ResponseCode = EnumResponse.Error;
            apiResponse.ResponseText = ApiResources.MensajeError;
            apiResponse.Data = employersResult.Errors;
        }
        else
        {
            apiResponse.ResponseCode = EnumResponse.Success;
            apiResponse.ResponseText = ApiResources.MensajeOk;
            apiResponse.Data = employersResult.Value;
        }
        
        return apiResponse;
    }

    [HttpGet("id/{Id}")]
    public async Task<ActionResult<ApiResponse>> GetById(Guid Id)
    {
        ApiResponse apiResponse = new ApiResponse();
        var employerResult = await _mediator.Send(new ReadByIdEmployeCommand(Id));
        
        if (employerResult.IsError)
        {
            apiResponse.ResponseCode = EnumResponse.Error;
            apiResponse.ResponseText = ApiResources.MensajeError;
            apiResponse.Data = employerResult.Errors;
        }
        else
        {
            apiResponse.ResponseCode = EnumResponse.Success;
            apiResponse.ResponseText = ApiResources.MensajeOk;
            apiResponse.Data = employerResult.Value;
        }
        
        return apiResponse;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> Create([FromBody] CreateEmployeCommand command)
    {
        ApiResponse apiResponse = new ApiResponse();
        var createEmployeResult = await _mediator.Send(command);
        
        if(createEmployeResult.IsError)
        {
            apiResponse.ResponseCode = EnumResponse.Error;
            apiResponse.ResponseText = ApiResources.MensajeError; 
            apiResponse.Data = createEmployeResult.Errors;
        }
        else
        {
            apiResponse.ResponseCode = EnumResponse.Success;
            apiResponse.ResponseText = ApiResources.MensajeOk;
            apiResponse.Data = createEmployeResult.Value;
        }

        return apiResponse;
    }

    [HttpPut("id/{Id}")]
    public async Task<ActionResult<ApiResponse>> Update(Guid Id, [FromBody] UpdateEmployeCommand command)
    {
        ApiResponse apiResponse = new ApiResponse();
        var updateResult = await _mediator.Send(command);
        
        if (updateResult.IsError)
        {
            apiResponse.ResponseCode = EnumResponse.Error;
            apiResponse.ResponseText = ApiResources.MensajeError;
            apiResponse.Data = updateResult.Errors;
        }
        else
        {
            apiResponse.ResponseCode = EnumResponse.Success;
            apiResponse.ResponseText = ApiResources.MensajeOk;
            apiResponse.Data = updateResult.Value;
        }
        
        return apiResponse;
    }

    [HttpDelete]
    public async Task<ActionResult<ApiResponse>> Delete(Guid Id)
    {
        ApiResponse apiResponse = new ApiResponse();
        var deleteResult = await _mediator.Send(new DeleteEmployeCommand(Id));
        
        if (deleteResult.IsError)
        {
            apiResponse.ResponseCode = EnumResponse.Error;
            apiResponse.ResponseText = ApiResources.MensajeError;
            apiResponse.Data = deleteResult.Errors;
        }
        else
        {
            apiResponse.ResponseCode = EnumResponse.Success;
            apiResponse.ResponseText = ApiResources.MensajeOk;
            apiResponse.Data = deleteResult.Value;
        }

        return apiResponse;
    }
}