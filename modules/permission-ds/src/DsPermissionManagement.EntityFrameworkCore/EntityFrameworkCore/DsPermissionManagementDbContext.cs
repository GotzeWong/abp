using DsPermissionManagement.Permissions;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DsPermissionManagement.EntityFrameworkCore
{
    [ConnectionStringName(DsPermissionManagementDbProperties.ConnectionStringName)]
    public class DsPermissionManagementDbContext : AbpDbContext<DsPermissionManagementDbContext>, IDsPermissionManagementDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public DbSet<DsPermission> DsPermissions { get; set; }

        public DsPermissionManagementDbContext(DbContextOptions<DsPermissionManagementDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureDsPermissionManagement();
        }
    }
}