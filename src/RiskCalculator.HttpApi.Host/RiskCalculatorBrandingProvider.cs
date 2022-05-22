using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace RiskCalculator;

[Dependency(ReplaceServices = true)]
public class RiskCalculatorBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "RiskCalculator";
}
