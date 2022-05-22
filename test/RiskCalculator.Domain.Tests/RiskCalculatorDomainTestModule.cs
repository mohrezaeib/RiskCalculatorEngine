using RiskCalculator.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace RiskCalculator;

[DependsOn(
    typeof(RiskCalculatorEntityFrameworkCoreTestModule)
    )]
public class RiskCalculatorDomainTestModule : AbpModule
{

}
