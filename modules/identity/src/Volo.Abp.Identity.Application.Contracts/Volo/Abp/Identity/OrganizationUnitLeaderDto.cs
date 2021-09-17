using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.Identity
{
    public class OrganizationUnitLeaderDto
    {
        public Guid OrgId { get; set; }
        public Guid UserId { get; set; }
        public bool isLeader { get; set; }
    }
}
