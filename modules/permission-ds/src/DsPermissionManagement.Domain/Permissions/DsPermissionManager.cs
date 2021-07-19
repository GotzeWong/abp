using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace DsPermissionManagement.Permissions
{
    public class DsPermissionManager:DomainService
    {
        private readonly IDsPermissionRepository _dsPermissionRepository;

        public DsPermissionManager(IDsPermissionRepository dsPermissionRepository)
        {
            _dsPermissionRepository = dsPermissionRepository;
        }

        public async Task<DsPermission> CreateAsync(Guid? parentId, string name, string discription, DsPermissionStatus status,Guid? tenantId,string providers)
        {
            if (parentId == null)
            {
                var existingPermission = await _dsPermissionRepository.FindByNameAsync(name);
                if (existingPermission != null)
                {
                    throw new BusinessException();
                }
            }
            else
            {
                var existingPermission = await _dsPermissionRepository.FindChildrenByNameAsync(parentId, name);
                if (existingPermission != null)
                {
                    throw new BusinessException();
                }
            }

            return new DsPermission(
                GuidGenerator.Create(),
                parentId,
                name,
                discription,
                status,
                tenantId,
                providers
                );
        }
    }
}
