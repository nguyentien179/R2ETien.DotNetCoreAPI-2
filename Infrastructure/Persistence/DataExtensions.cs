using System;
using Microsoft.EntityFrameworkCore;

namespace _netcore_2.Infrastructure.Persistence;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PersonContext>();
        dbContext.Database.Migrate();

        try
        {
            dbContext.SeedData();
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine($"Error seeding data: {ex.Message}");
        }
    }
}
