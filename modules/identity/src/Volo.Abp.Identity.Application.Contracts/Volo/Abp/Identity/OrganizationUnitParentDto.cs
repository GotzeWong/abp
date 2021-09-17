using System;
using System.Collections.Generic;

namespace Volo.Abp.Identity
{
    public class OrganizationUnitParentDto : OrganizationUnitChildDto
    {
        public long AllMembers { get; set; }
        public long Total { get; set; }
        public List<OrganizationUnitParentDto> Children { get; set; }
        public List<IdentityUserLeadertDto> Leaders { get; set; }
    }
}
