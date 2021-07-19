using Localization.Resources.AbpUi;
using DsPermissionManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace DsPermissionManagement
{
    [DependsOn(
        typeof(DsPermissionManagementApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class DsPermissionManagementHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DsPermissionManagementHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<DsPermissionManagementResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
