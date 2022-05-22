using RiskCalculator.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace RiskCalculator.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class RiskCalculatorController : AbpControllerBase
{
    protected RiskCalculatorController()
    {
        LocalizationResource = typeof(RiskCalculatorResource);
    }
}
