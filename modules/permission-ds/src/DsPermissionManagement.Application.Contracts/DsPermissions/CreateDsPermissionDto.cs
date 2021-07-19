using DsPermissionManagement.Permissions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DsPermissionManagement.DsPermissions
{
    public class CreateDsPermissionDto: AuditedEntityDto<Guid>
    {
        public Guid? ParentId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Discription { get; set; }

        [Required]
        public DsPermissionStatus Status { get; set; }

        public Guid? TenantId { get; set; }

        public string Providers { get; set; }
    }
}
