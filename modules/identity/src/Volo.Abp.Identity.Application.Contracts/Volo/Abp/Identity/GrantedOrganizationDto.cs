using System;
using System.Collections.Generic;

namespace Volo.Abp.Identity
{
    public class GrantedOrganizationDto
    {
        public Guid Id { get; set; }
        public List<OrganizationDto> Orgs { get; set; }
        public GrantedOrganizationDto(Guid id)
        {
            this.Id = id;
        }

    }
}
