using RiskCalculator.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace RiskCalculator.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(RiskCalculatorEntityFrameworkCoreModule),
    typeof(RiskCalculatorApplicationContractsModule)
    )]
public class RiskCalculatorDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
