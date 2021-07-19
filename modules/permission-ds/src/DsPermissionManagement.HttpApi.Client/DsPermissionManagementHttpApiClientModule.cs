using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace DsPermissionManagement
{
    [DependsOn(
        typeof(DsPermissionManagementApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class DsPermissionManagementHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "DsPermissionManagement";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(DsPermissionManagementApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
