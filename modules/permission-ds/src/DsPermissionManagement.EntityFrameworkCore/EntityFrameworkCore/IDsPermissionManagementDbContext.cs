using DsPermissionManagement.Permissions;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DsPermissionManagement.EntityFrameworkCore
{
    [ConnectionStringName(DsPermissionManagementDbProperties.ConnectionStringName)]
    public interface IDsPermissionManagementDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */

        DbSet<DsPermission> DsPermissions { get; }
    }
}