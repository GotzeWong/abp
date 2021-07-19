using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace DsPermissionManagement.EntityFrameworkCore
{
    public class DsPermissionManagementModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public DsPermissionManagementModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}