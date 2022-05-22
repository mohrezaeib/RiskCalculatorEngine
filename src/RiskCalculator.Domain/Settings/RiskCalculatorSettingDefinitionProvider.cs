using Volo.Abp.Settings;

namespace RiskCalculator.Settings;

public class RiskCalculatorSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(RiskCalculatorSettings.MySetting1));
    }
}
