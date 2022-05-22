using Volo.Abp.Modularity;

namespace RiskCalculator;

[DependsOn(
    typeof(RiskCalculatorApplicationModule),
    typeof(RiskCalculatorDomainTestModule)
    )]
public class RiskCalculatorApplicationTestModule : AbpModule
{

}
