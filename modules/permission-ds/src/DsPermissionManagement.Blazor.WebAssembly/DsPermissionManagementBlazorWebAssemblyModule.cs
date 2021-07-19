using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace DsPermissionManagement.Blazor.WebAssembly
{
    [DependsOn(
        typeof(DsPermissionManagementBlazorModule),
        typeof(DsPermissionManagementHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class DsPermissionManagementBlazorWebAssemblyModule : AbpModule
    {
        
    }
}