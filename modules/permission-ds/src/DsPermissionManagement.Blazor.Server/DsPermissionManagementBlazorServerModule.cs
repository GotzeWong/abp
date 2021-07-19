using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace DsPermissionManagement.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(DsPermissionManagementBlazorModule)
        )]
    public class DsPermissionManagementBlazorServerModule : AbpModule
    {
        
    }
}