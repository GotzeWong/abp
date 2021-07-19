using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DsPermissionManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(DsPermissionManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class DsPermissionManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<DsPermissionManagementDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */

                options.AddDefaultRepositories<IDsPermissionManagementDbContext>();
            });
        }
    }
}