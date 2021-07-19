using DsPermissionManagement.Localization;
using Volo.Abp.Application.Services;

namespace DsPermissionManagement
{
    public abstract class DsPermissionManagementAppService : ApplicationService
    {
        protected DsPermissionManagementAppService()
        {
            LocalizationResource = typeof(DsPermissionManagementResource);
            ObjectMapperContext = typeof(DsPermissionManagementApplicationModule);
        }
    }
}
