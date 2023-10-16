using FluentValidation;

namespace Application.Customers.Delete;

public class DeleteCustomerComandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerComandValidator()
    {
        RuleFor(r => r.Id).NotEmpty();
    }
}