using Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependecyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReferences>();
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviors<,>));

        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReferences>();
        
        return services;
    }
}