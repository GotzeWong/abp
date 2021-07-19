using DsPermissionManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace DsPermissionManagement
{
    public abstract class DsPermissionManagementController : AbpController
    {
        protected DsPermissionManagementController()
        {
            LocalizationResource = typeof(DsPermissionManagementResource);
        }
    }
}
