using DsPermissionManagement.Permissions;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace DsPermissionManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(DsPermissionManagementDomainSharedModule)
    )]
    public class DsPermissionManagementDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
