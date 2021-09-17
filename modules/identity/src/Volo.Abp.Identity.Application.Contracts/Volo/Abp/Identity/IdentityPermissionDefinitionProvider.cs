using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;

namespace Volo.Abp.Identity
{
    public class IdentityPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var identityGroup = context.AddGroup(IdentityPermissions.GroupName, L("Permission:IdentityManagement"));

            var rolesPermission = identityGroup.AddPermission(IdentityPermissions.Roles.Default, L("Permission:RoleManagement"));
            rolesPermission.AddChild(IdentityPermissions.Roles.Create, L("Permission:Create"));
            rolesPermission.AddChild(IdentityPermissions.Roles.Update, L("Permission:Edit"));
            rolesPermission.AddChild(IdentityPermissions.Roles.Delete, L("Permission:Delete"));
            rolesPermission.AddChild(IdentityPermissions.Roles.Export, L("Permission:Export"));
            rolesPermission.AddChild(IdentityPermissions.Roles.ManagePermissions, L("Permission:ChangePermissions"));

            var usersPermission = identityGroup.AddPermission(IdentityPermissions.Users.Default, L("Permission:UserManagement"));
            usersPermission.AddChild(IdentityPermissions.Users.Create, L("Permission:Create"));
            usersPermission.AddChild(IdentityPermissions.Users.Update, L("Permission:Edit"));
            usersPermission.AddChild(IdentityPermissions.Users.Delete, L("Permission:Delete"));
            usersPermission.AddChild(IdentityPermissions.Users.Export, L("Permission:Export"));
            usersPermission.AddChild(IdentityPermissions.Users.ManagePermissions, L("Permission:ChangePermissions"));

            var oirganizationsPermission = identityGroup.AddPermission(IdentityPermissions.Organizations.Default, L("Permission:OrganizationManagement"));
            oirganizationsPermission.AddChild(IdentityPermissions.Organizations.Create, L("Permission:Create"));
            oirganizationsPermission.AddChild(IdentityPermissions.Organizations.Update, L("Permission:Edit"));
            oirganizationsPermission.AddChild(IdentityPermissions.Organizations.Delete, L("Permission:Delete"));
            oirganizationsPermission.AddChild(IdentityPermissions.Organizations.Export, L("Permission:Export"));
            oirganizationsPermission.AddChild(IdentityPermissions.Organizations.OUManagement, L("Permission:OUManagement"));
            oirganizationsPermission.AddChild(IdentityPermissions.Organizations.ManagePermissions, L("Permission:ChangePermissions"));

            var temporaryAccessPermissionPermission = identityGroup.AddPermission(IdentityPermissions.TemporaryAccessPermissions.Default, L("Permission:TemporaryAccessPermissionManagement"));
            temporaryAccessPermissionPermission.AddChild(IdentityPermissions.TemporaryAccessPermissions.Create, L("Permission:Create"));
            temporaryAccessPermissionPermission.AddChild(IdentityPermissions.TemporaryAccessPermissions.Update, L("Permission:Edit"));
            temporaryAccessPermissionPermission.AddChild(IdentityPermissions.TemporaryAccessPermissions.Delete, L("Permission:Delete"));
            temporaryAccessPermissionPermission.AddChild(IdentityPermissions.TemporaryAccessPermissions.ManagePermissions, L("Permission:ChangePermissions"));

            identityGroup
                .AddPermission(IdentityPermissions.UserLookup.Default, L("Permission:UserLookup"))
                .WithProviders(ClientPermissionValueProvider.ProviderName);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }
    }
}
