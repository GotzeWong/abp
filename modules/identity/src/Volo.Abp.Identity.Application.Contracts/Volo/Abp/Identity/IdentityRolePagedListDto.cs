using AgentHub.Shared;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Volo.Abp.Identity
{
    public class IdentityRolePagedListDto : PagedSortedAndFilteredResultRequestDto
    {
        public string Name { get; set; }
    }
}