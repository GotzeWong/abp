using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DsPermissionManagement.DsPermissions
{
    public interface IDsPermissionAppService:IApplicationService
    {
        Task<DsPermissionDto> CreateAsync(CreateDsPermissionDto input);

        Task DeleteAsync(Guid id);

        /// <summary>
        /// 获取默认权限列表
        /// </summary>
        /// <returns></returns>
        Task<List<DsPermissionDto>> GetListAsync();

        /// <summary>
        /// 获取对应租户的权限列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<List<DsPermissionDto>> GetListAsync(Guid tenantId);

        Task<DsPermissionDto> UpdateAsync(Guid id, UpdateDsPermissionDto input);
    }
}
