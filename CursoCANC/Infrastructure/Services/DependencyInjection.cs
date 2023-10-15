using Application.Data;
using Domain.Customers;
using Domain.Primitives;
using Infrastructure.Persistence;
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

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        
        return services;
    }
}