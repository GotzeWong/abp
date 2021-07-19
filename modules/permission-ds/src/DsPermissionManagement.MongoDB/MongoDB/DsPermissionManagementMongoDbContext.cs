using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace DsPermissionManagement.MongoDB
{
    [ConnectionStringName(DsPermissionManagementDbProperties.ConnectionStringName)]
    public class DsPermissionManagementMongoDbContext : AbpMongoDbContext, IDsPermissionManagementMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureDsPermissionManagement();
        }
    }
}