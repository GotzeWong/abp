using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Volo.Abp.Identity
{
    public interface ITemporaryAccessPermissionRepository : IBasicRepository<TemporaryAccessPermission>
    {
        Task<TemporaryAccessPermission> GetAsync(Guid id);

        Task<long> GetCountAsync(
            Guid? grantUserId,
            string grantUserName,
            Guid? temporaryUserId,
            string temporaryUserName,
            DateTime? startTime,
            DateTime? endTime,
            bool? IsOpen,
            string filter,
            int skipCount,
            int maxResultCount,
            string sorting);

        Task<List<TemporaryAccessPermission>> GetPagedListAsync(
            Guid? grantUserId,
            string grantUserName,
            Guid? temporaryUserId,
            string temporaryUserName,
            DateTime? startTime,
            DateTime? endTime,
            bool? IsOpen,
            string filter,
            int skipCount,
            int maxResultCount,
            string sorting);

        Task<List<Guid>> GetAllByTemporaryUserIdAsync(Guid temporaryUserId);
    }
}
