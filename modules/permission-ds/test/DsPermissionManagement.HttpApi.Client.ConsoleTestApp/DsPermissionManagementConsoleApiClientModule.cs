using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace DsPermissionManagement
{
    [DependsOn(
        typeof(DsPermissionManagementHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class DsPermissionManagementConsoleApiClientModule : AbpModule
    {
        
    }
}
