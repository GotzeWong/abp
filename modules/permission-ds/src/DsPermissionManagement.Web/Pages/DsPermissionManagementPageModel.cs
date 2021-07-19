using DsPermissionManagement.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace DsPermissionManagement.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class DsPermissionManagementPageModel : AbpPageModel
    {
        protected DsPermissionManagementPageModel()
        {
            LocalizationResourceType = typeof(DsPermissionManagementResource);
            ObjectMapperContext = typeof(DsPermissionManagementWebModule);
        }
    }
}