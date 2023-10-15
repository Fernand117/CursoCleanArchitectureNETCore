using FluentValidation;
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

        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReferences>();
        
        return services;
    }
}