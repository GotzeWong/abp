using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace DsPermissionManagement
{
    [DependsOn(
        typeof(DsPermissionManagementDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class DsPermissionManagementApplicationContractsModule : AbpModule
    {

    }
}
