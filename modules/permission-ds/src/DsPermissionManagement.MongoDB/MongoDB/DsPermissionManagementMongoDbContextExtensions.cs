using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace DsPermissionManagement.MongoDB
{
    public static class DsPermissionManagementMongoDbContextExtensions
    {
        public static void ConfigureDsPermissionManagement(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new DsPermissionManagementMongoModelBuilderConfigurationOptions(
                DsPermissionManagementDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}