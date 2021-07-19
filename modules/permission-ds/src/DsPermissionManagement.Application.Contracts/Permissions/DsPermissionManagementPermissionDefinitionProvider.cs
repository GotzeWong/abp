using DsPermissionManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace DsPermissionManagement.Permissions
{
    public class DsPermissionManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            // 数据库根据providerKey providerValue获取Definition

            var myGroup = context.AddGroup(DsPermissionManagementPermissions.GroupName, L("Permission:DsPermissionManagement"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DsPermissionManagementResource>(name);
        }
    }
}