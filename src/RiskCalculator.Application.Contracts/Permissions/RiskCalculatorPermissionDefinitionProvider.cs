using RiskCalculator.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace RiskCalculator.Permissions;

public class RiskCalculatorPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(RiskCalculatorPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(RiskCalculatorPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<RiskCalculatorResource>(name);
    }
}
