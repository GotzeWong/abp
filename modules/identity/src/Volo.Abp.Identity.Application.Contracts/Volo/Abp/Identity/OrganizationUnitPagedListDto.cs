using AgentHub.Shared;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.Identity
{
    public class OrganizationUnitPagedListDto : PagedSortedAndFilteredResultRequestDto
    {
        public string Code { get; internal set; }
        public string DisplayName { get; set; }
        public OrganizationUnitStatus Status { get; set; } = OrganizationUnitStatus.Null;
        public string Remark { get; set; }
    }
}
