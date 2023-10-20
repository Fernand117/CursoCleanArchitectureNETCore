using Application.Customers.Create;
using Application.Customers.Delete;
using Application.Customers.ReadAll;
using Application.Customers.ReadById;
using Application.Customers.Update;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CCANC.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ApiController
{
    private readonly ISender _mediator;

    public CustomersController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customersResult = await _mediator.Send(new ReadAllCustomerCommand());

        return customersResult.Match(
            customers => Ok(customers),
            errors => Problem(errors));
    }

    [HttpGet("id/{Id}")]
    public async Task<IActionResult> GetById(Guid Id)
    {
        var customerResult = await _mediator.Send(new ReadByIdCommand(Id));

        return customerResult.Match(
            customer => Ok(customer),
            error => Problem(error));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var createCustomerResult = await _mediator.Send(command);

        return createCustomerResult.Match(customer => Ok(), errors => Problem(errors));
    }

    [HttpPut("id/{Id}")]
    public async Task<IActionResult> Update(Guid Id, [FromBody] UpdateCustomerCommand command)
    {
        if (command.Id != Id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Customer.UpdateInfo", "The request ID does not match with the url ID.")
            };
            return Problem();
        }

        var updateResult = await _mediator.Send(command);

        return updateResult.Match(
            customerId => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("id/{Id}")]
    public async Task<IActionResult> Delete(Guid Id)
    {
        var deleteResult = await _mediator.Send(new DeleteCustomerCommand(Id));

        return deleteResult.Match(
            customerId => NoContent(),
            errors => Problem(errors));
    }
}