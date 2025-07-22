using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StocksApi.Data;

namespace StocksApi.Extensions;

public static class AppConfigurationExtensions
{
    // "dotnet ef migrations add [MIGRATION NAME]"
    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var dbContext = services.GetRequiredService<ApplicationDbContext>();
                Console.WriteLine("Attempting to apply migrations...");
                await dbContext.Database.MigrateAsync();
                Console.WriteLine("Migrations applied successfully or no pending migrations.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
