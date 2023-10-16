using FluentValidation;

namespace Application.Customers.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
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