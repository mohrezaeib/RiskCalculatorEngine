using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RiskCalculator.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class RiskCalculatorDbContextFactory : IDesignTimeDbContextFactory<RiskCalculatorDbContext>
{
    public RiskCalculatorDbContext CreateDbContext(string[] args)
    {
        RiskCalculatorEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<RiskCalculatorDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new RiskCalculatorDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../RiskCalculator.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
