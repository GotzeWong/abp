using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.Identity
{
    public class OrganizationUnitBindDto : OrganizationUnitUnbindDto
    {
        public Guid[] OrgLeaderIds { get; set; }
    }
}
