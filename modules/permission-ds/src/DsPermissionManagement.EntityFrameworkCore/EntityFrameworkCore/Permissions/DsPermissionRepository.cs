using DsPermissionManagement.Permissions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DsPermissionManagement.EntityFrameworkCore.Permissions
{
    public class DsPermissionRepository : EfCoreRepository<DsPermissionManagementDbContext, DsPermission, Guid>, IDsPermissionRepository
    {
        public DsPermissionRepository(IDbContextProvider<DsPermissionManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<DsPermission> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(permission => permission.Name == name);
        }

        public async Task<DsPermission> FindChildrenByNameAsync(Guid? parentId, string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(permission => permission.ParentId == parentId && permission.Name == name);
        }

        public async Task<List<DsPermission>> GetChildernListAsync(Guid parentId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(permission => permission.ParentId == parentId).ToListAsync();
        }

        public async Task<List<DsPermission>> GetPermissionList()
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(permission => permission.TenantId == null).ToListAsync();
        }
    }
}
