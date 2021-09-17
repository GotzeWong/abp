using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.Identity
{
    public class OrganizationUnitDto : ExtensibleFullAuditedEntityDto<Guid>, IMultiTenant, IHasConcurrencyStamp
    {
        public Guid? TenantId { get; set; }
        public Guid? ParentId { get; set; }
        public string Code { get; internal set; }
        public string DisplayName { get; set; }
        public OrganizationUnitStatus Status { get; set; }
        public int OrderNo { get; set; }
        public string Remark { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
