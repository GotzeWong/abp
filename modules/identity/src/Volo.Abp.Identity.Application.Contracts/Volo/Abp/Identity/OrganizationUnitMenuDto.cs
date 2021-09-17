using System.Collections.Generic;

namespace Volo.Abp.Identity
{
    public class OrganizationUnitMenuDto : OrganizationUnitSubMenuBaseDto
    {
        public string name { get; set; }
        public string type { get; set; }
        public List<OrganizationUnitSubMenuDto> submenu { get; set; }

        public OrganizationUnitMenuDto(string _name, string _type)
        {
            name = _name;
            type = _type;
        }
    }

    public class OrganizationUnitSubMenuDto : OrganizationUnitSubMenuBaseDto
    {
        public List<OrganizationUnitSubMenuDto>? submenu { get; set; }
    }

    public class OrganizationUnitSubMenuBaseDto
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
