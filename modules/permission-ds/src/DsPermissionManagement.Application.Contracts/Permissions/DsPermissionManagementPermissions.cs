using Volo.Abp.Reflection;

namespace DsPermissionManagement.Permissions
{
    public class DsPermissionManagementPermissions
    {
        public const string GroupName = "DsPermissionManagement";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(DsPermissionManagementPermissions));
        }
    }
}