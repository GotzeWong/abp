using DsPermissionManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DsPermissionManagement
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(DsPermissionManagementEntityFrameworkCoreTestModule)
        )]
    public class DsPermissionManagementDomainTestModule : AbpModule
    {
        
    }
}
