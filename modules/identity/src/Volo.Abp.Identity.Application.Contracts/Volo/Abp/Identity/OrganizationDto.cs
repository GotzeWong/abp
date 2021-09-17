using System;
using System.Collections.Generic;

namespace Volo.Abp.Identity
{
    public class OrganizationDto
    {
        public string Code { get; set; }
        public bool isLeader { get; set; }
    }
}
