using System;
using System.Collections.Generic;

namespace Volo.Abp.Identity
{
    public class OrganizationUnitParentDto : OrganizationUnitChildDto
    {
        public List<OrganizationUnitChildDto> Children { get; set; }
    }
}
