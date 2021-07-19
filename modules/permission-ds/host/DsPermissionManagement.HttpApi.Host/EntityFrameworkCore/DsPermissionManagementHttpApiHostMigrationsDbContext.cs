using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DsPermissionManagement.EntityFrameworkCore
{
    public class DsPermissionManagementHttpApiHostMigrationsDbContext : AbpDbContext<DsPermissionManagementHttpApiHostMigrationsDbContext>
    {
        public DsPermissionManagementHttpApiHostMigrationsDbContext(DbContextOptions<DsPermissionManagementHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureDsPermissionManagement();
        }
    }
}
