using FluentValidation;

namespace Application.Customers.Update;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(rule => rule.Id).NotEmpty();

        RuleFor(rule => rule.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithName("Nombre");

        RuleFor(rule => rule.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255)
            .WithName("Correo");

        RuleFor(rule => rule.PhoneNumber)
            .NotEmpty()
            .MaximumLength(10);
    }
}