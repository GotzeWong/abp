using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace DsPermissionManagement.MongoDB
{
    [ConnectionStringName(DsPermissionManagementDbProperties.ConnectionStringName)]
    public interface IDsPermissionManagementMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
