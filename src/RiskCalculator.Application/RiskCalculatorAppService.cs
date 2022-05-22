using System;
using System.Collections.Generic;
using System.Text;
using RiskCalculator.Localization;
using Volo.Abp.Application.Services;

namespace RiskCalculator;

/* Inherit your application services from this class.
 */
public abstract class RiskCalculatorAppService : ApplicationService
{
    protected RiskCalculatorAppService()
    {
        LocalizationResource = typeof(RiskCalculatorResource);
    }
}
