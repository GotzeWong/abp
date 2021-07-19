using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace DsPermissionManagement.MongoDB
{
    public class DsPermissionManagementMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public DsPermissionManagementMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}