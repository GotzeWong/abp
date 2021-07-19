using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Authorization.Permissions;

namespace DsPermissionManagement.Permissions
{
    public class DsPermissionDefinitionManager : PermissionDefinitionManager
    {
        private readonly IDsPermissionRepository _dsPermissionRepository;

        public DsPermissionDefinitionManager(IOptions<AbpPermissionOptions> options, IServiceProvider serviceProvider,IDsPermissionRepository dsPermissionRepository) : base(options, serviceProvider)
        {
            _dsPermissionRepository = dsPermissionRepository;
        }

        public override IReadOnlyList<PermissionDefinition> GetPermissions()
        {
            return null;
        }

        
    }
}
