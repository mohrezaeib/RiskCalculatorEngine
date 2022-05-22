using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace RiskCalculator.Data;

/* This is used if database provider does't define
 * IRiskCalculatorDbSchemaMigrator implementation.
 */
public class NullRiskCalculatorDbSchemaMigrator : IRiskCalculatorDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
