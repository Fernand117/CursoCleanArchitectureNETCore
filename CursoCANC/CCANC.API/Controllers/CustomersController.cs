using Application.Customers.Create;
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
}