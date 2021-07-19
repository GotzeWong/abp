using Volo.Abp.Modularity;

namespace DsPermissionManagement
{
    [DependsOn(
        typeof(DsPermissionManagementApplicationModule),
        typeof(DsPermissionManagementDomainTestModule)
        )]
    public class DsPermissionManagementApplicationTestModule : AbpModule
    {

    }
}
