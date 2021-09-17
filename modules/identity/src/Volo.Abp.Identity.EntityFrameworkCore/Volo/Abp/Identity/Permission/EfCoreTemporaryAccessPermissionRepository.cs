using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeZoneConverter;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Volo.Abp.Identity.EntityFrameworkCore
{
    public class EfCoreTemporaryAccessPermissionRepository : EfCoreRepository<IIdentityDbContext, TemporaryAccessPermission>, ITemporaryAccessPermissionRepository
    {
        public EfCoreTemporaryAccessPermissionRepository(IDbContextProvider<IIdentityDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Guid>> GetAllByTemporaryUserIdAsync(Guid temporaryUserId)
        {
            var currentTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TZConvert.GetTimeZoneInfo("China Standard Time"));

            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(temporary => temporary.TemporaryUser.Id == temporaryUserId 
                                            && temporary.IsOpen == true 
                                            && (currentTime >= temporary.StartTime&& currentTime <= temporary.EndTime))
                              .Select(temporary=>temporary.GrantUser.Id)
                              .ToListAsync();
        }

        public async Task<TemporaryAccessPermission> GetAsync(Guid id)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(temporary => temporary.Id == id)
                              .FirstOrDefaultAsync();
        }


        public async Task<long> GetCountAsync(
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
            string sorting)
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet.Include(temporary => temporary.GrantUser)
                              .Include(temporary => temporary.TemporaryUser)
                              .WhereIf(!filter.IsNullOrWhiteSpace(), temporary => temporary.GrantUser.Name.Contains(filter)
                                         || temporary.TemporaryUser.Name.Contains(filter)
                                         )
                              .WhereIf(grantUserId != null, temporary => temporary.GrantUser.Id == grantUserId)
                              .WhereIf(temporaryUserId != null, temporary => temporary.TemporaryUser.Id == temporaryUserId)
                              .WhereIf(!grantUserName.IsNullOrEmpty(), temporary => temporary.GrantUser.Name.Contains(grantUserName))
                              .WhereIf(!temporaryUserName.IsNullOrEmpty(), temporary => temporary.TemporaryUser.Name.Contains(temporaryUserName))
                              .WhereIf(startTime != null, temporary => temporary.StartTime >= startTime)
                              .WhereIf(endTime != null, temporary => temporary.EndTime <= endTime)
                              .WhereIf(IsOpen.HasValue, temporary => temporary.IsOpen == IsOpen)
                              .LongCountAsync();
        }

        public async Task<List<TemporaryAccessPermission>> GetPagedListAsync(
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
            string sorting)
        {
            var dbSet = await GetDbSetAsync();
            sorting = sorting.IsNullOrWhiteSpace() ? nameof(TemporaryAccessPermission.TemporaryUser.Id) : sorting;

            return await dbSet.Include(temporary => temporary.GrantUser)
                              .Include(temporary => temporary.TemporaryUser)
                              .WhereIf(!filter.IsNullOrWhiteSpace(), temporary => temporary.GrantUser.Name.Contains(filter)
                                         || temporary.TemporaryUser.Name.Contains(filter)
                                         )
                              .WhereIf(grantUserId != null, temporary => temporary.GrantUser.Id == grantUserId)
                              .WhereIf(temporaryUserId != null, temporary => temporary.TemporaryUser.Id == temporaryUserId)
                              .WhereIf(!grantUserName.IsNullOrEmpty(), temporary => temporary.GrantUser.Name.Contains(grantUserName))
                              .WhereIf(!temporaryUserName.IsNullOrEmpty(), temporary => temporary.TemporaryUser.Name.Contains(temporaryUserName))
                              .WhereIf(startTime != null, temporary => temporary.StartTime >= startTime)
                              .WhereIf(endTime != null, temporary => temporary.EndTime <= endTime)
                              .WhereIf(IsOpen.HasValue, temporary => temporary.IsOpen == IsOpen)
                              .PageBy(skipCount, maxResultCount)
                              .OrderBy(sorting)
                              .ToListAsync();
        }




    }
}
