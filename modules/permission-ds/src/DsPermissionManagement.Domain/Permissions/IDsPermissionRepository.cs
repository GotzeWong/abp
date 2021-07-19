using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DsPermissionManagement.Permissions
{
    public interface IDsPermissionRepository:IRepository<DsPermission,Guid>
    {
        Task<DsPermission> FindByNameAsync(string name);

        Task<DsPermission> FindChildrenByNameAsync(Guid? parentId, string name);

        Task<List<DsPermission>> GetChildernListAsync(Guid parentId);

        Task<List<DsPermission>> GetPermissionList();
    }
}
