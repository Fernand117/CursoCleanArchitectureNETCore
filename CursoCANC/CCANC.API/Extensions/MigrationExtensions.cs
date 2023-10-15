using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CCANC.API.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
        dbContext.Database.Migrate();
    }
}