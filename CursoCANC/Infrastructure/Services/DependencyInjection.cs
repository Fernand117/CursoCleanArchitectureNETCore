using Application.Data;
using Domain.Customers;
using Domain.Employes;
using Domain.Primitives;
using Domain.Users;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Configuration;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql(configuration.GetConnectionString("Connection")));
        
        services.AddScoped<IApplicationDBContext>(sp => sp.GetRequiredService<ApplicationDBContext>());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDBContext>());

        // AÑADIR LOS REPOSITORY CON SUS INTERFACES SIEMPRE QUE SE CREA UNO NUEVO
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IEmployeRepository, EmployeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}