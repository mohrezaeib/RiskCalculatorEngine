using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RiskCalculator.Data;
using Volo.Abp.DependencyInjection;

namespace RiskCalculator.EntityFrameworkCore;

public class EntityFrameworkCoreRiskCalculatorDbSchemaMigrator
    : IRiskCalculatorDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreRiskCalculatorDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the RiskCalculatorDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<RiskCalculatorDbContext>()
            .Database
            .MigrateAsync();
    }
}
