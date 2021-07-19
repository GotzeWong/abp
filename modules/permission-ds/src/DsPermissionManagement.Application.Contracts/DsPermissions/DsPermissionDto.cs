using DsPermissionManagement.Permissions;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DsPermissionManagement.DsPermissions
{
    public class DsPermissionDto: AuditedEntityDto<Guid>
    {
        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public string Discription { get; set; }

        public DsPermissionStatus Status { get; set; }

        public Guid? TenantId { get; set; }

        public string Providers { get; set; }
    }
}
