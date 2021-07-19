using DsPermissionManagement.Permissions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace DsPermissionManagement.DsPermissions
{
    public class DsPermissionAppService : DsPermissionManagementAppService, IDsPermissionAppService
    {
        private readonly DsPermissionManager _dsPermissionManager;
        private readonly IDsPermissionRepository _dsPermissionRepository;

        public DsPermissionAppService(DsPermissionManager dsPermissionManager,IDsPermissionRepository dsPermissionRepository)
        {
            _dsPermissionManager = dsPermissionManager;
            _dsPermissionRepository = dsPermissionRepository;
        }

        public async Task<DsPermissionDto> CreateAsync(CreateDsPermissionDto input)
        {
            var dsPermission = await _dsPermissionManager.CreateAsync(
                input.ParentId,
                input.Name,
                input.Discription,
                input.Status,
                input.TenantId,
                input.Providers
                );
            await _dsPermissionRepository.InsertAsync(dsPermission);
            return ObjectMapper.Map<DsPermission, DsPermissionDto>(dsPermission);
        }

        public async Task DeleteAsync(Guid id)
        {
            var childerList = await _dsPermissionRepository.GetChildernListAsync(id);
            if (childerList.Count > 0)
            {
                throw new BusinessException();
            }
            await _dsPermissionRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<List<DsPermissionDto>> GetListAsync()
        {
            var dsPermissions = await _dsPermissionRepository.GetListAsync(dsPermissions => dsPermissions.TenantId == null);
            return ObjectMapper.Map<List<DsPermission>, List<DsPermissionDto>>(dsPermissions);
        }

        /// <summary>
        /// 获取租户对应的权限列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<List<DsPermissionDto>> GetListAsync(Guid tenantId)
        {
            var dsPermissions = await _dsPermissionRepository.GetListAsync(dsPermission => dsPermission.TenantId != null && dsPermission.TenantId == tenantId);
            return ObjectMapper.Map<List<DsPermission>, List<DsPermissionDto>>(dsPermissions);
        }

        public async Task<DsPermissionDto> UpdateAsync(Guid id, UpdateDsPermissionDto input)
        {
            var permission = await _dsPermissionRepository.GetAsync(id);
            permission.SetParentId(input.ParentId);
            permission.SetName(input.Name);
            permission.SetDiscription(input.Discription);
            permission.SetStatus(input.Status);
            return ObjectMapper.Map<DsPermission, DsPermissionDto>(permission);
        }
    }
}
