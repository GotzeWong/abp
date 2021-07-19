using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DsPermissionManagement.EntityFrameworkCore
{
    public class DsPermissionManagementHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<DsPermissionManagementHttpApiHostMigrationsDbContext>
    {
        public DsPermissionManagementHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<DsPermissionManagementHttpApiHostMigrationsDbContext>()
                .UseMySql(configuration.GetConnectionString("DsPermissionManagement"),ServerVersion.AutoDetect(configuration.GetConnectionString("DsPermissionManagement")));

            return new DsPermissionManagementHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
