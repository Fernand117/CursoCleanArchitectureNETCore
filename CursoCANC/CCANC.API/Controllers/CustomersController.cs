using Application.Customers.Create;
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var createCustomerResult = await _mediator.Send(command);

        return createCustomerResult.Match(customer => Ok(), errors => Problem(errors));
    }

    [HttpPut("/id/{id}")]
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
}